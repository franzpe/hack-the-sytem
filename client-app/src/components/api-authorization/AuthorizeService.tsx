import { User, UserManager, UserManagerSettings, WebStorageStateStore } from 'oidc-client';
import { ApplicationPaths, ApplicationName } from './ApiAuthorizationConstants';
import axios from 'axios';

interface Callback {
  callback: Function;
  subscription: number;
}

export interface AuthState {
  returnUrl?: string;
  message?: string;
}

export interface AuthResult {
  status: string;
  message?: string;
  state?: AuthState;
}

export class AuthorizeService {
  _callbacks: Callback[];
  _nextSubscriptionId = 0;
  _isAuthenticated = false;
  _user: User | undefined;
  _userManager: UserManager | undefined;

  constructor() {
    this._callbacks = [];
    this._user = undefined;
    this._userManager = undefined;
  }

  async isAuthenticated() {
    const user = await this.getUser();
    return !!user;
  }

  async getUser() {
    if (this._user && this._user.profile) {
      return this._user.profile;
    }

    await this.ensureUserManagerInitialized();
    const user = await this._userManager?.getUser();
    return user && user.profile;
  }

  async getAccessToken() {
    await this.ensureUserManagerInitialized();
    const user = await this._userManager?.getUser();
    return user && user.access_token;
  }

  // We try to authenticate the user in three different ways:
  // 1) We try to see if we can authenticate the user silently. This happens
  //    when the user is already logged in on the IdP and is done using a hidden iframe
  //    on the client.
  // 2) We try to authenticate the user using a PopUp Window. This might fail if there is a
  //    Pop-Up blocker or the user has disabled PopUps.
  // 3) If the two methods above fail, we redirect the browser to the IdP to perform a traditional
  //    redirect flow.
  async signIn(state?: AuthState) {
    await this.ensureUserManagerInitialized();
    try {
      console.log(this._userManager);
      const silentUser = await this._userManager?.signinSilent(this.createArguments());
      this.updateState(silentUser);
      return this.success(state);
    } catch (silentError) {
      // User might not be authenticated, fallback to redirect
      console.log('Silent authentication error: ', silentError);

      try {
        await this._userManager?.signinRedirect(this.createArguments(state));
        return this.redirect();
      } catch (redirectError) {
        console.log('Redirect authentication error: ', redirectError);
        return this.error(redirectError);
      }
    }
  }

  async completeSignIn(url: string) {
    try {
      const user = await this._userManager?.signinCallback(url);
      this.updateState(user);
      return this.success(user && user.state);
    } catch (error) {
      console.log('There was an error signing in: ', error);
      return this.error('There was an error signing in.');
    }
  }

  // We try to sign out the user in two different ways:
  // 1) We try to do a sign-out using a PopUp Window. This might fail if there is a
  //    Pop-Up blocker or the user has disabled PopUps.
  // 2) If the method above fails, we redirect the browser to the IdP to perform a traditional
  //    post logout redirect flow.
  async signOut(state?: AuthState) {
    try {
      await this._userManager?.signoutRedirect(this.createArguments(state));
      return this.redirect();
    } catch (redirectSignOutError) {
      console.log('Redirect signout error: ', redirectSignOutError);
      return this.error(redirectSignOutError);
    }
  }

  async completeSignOut(url: string) {
    try {
      const response = await this._userManager?.signoutCallback(url);
      this.updateState(undefined);
      return this.success(response && response.state);
    } catch (error) {
      console.log(`There was an error trying to log out '${error}'.`);
      return this.error(error);
    }
  }

  updateState(user: User | undefined) {
    this._user = user;
    this._isAuthenticated = !!this._user;
    if (user?.access_token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${user.access_token}`;
    }
    this.notifySubscribers();
  }

  subscribe(callback: Function) {
    this._callbacks.push({ callback, subscription: this._nextSubscriptionId++ });
    return this._nextSubscriptionId - 1;
  }

  unsubscribe(subscriptionId: number) {
    const subscriptionIndex = this._callbacks
      .map((element, index) =>
        element.subscription === subscriptionId ? { found: true, index } : { found: false }
      )
      .filter(element => element.found === true);
    if (subscriptionIndex.length !== 1 || subscriptionIndex[0].index === undefined) {
      throw new Error(`Found an invalid number of subscriptions ${subscriptionIndex.length}`);
    }

    this._callbacks.splice(subscriptionIndex[0].index, 1);
  }

  notifySubscribers() {
    for (let i = 0; i < this._callbacks.length; i++) {
      const callback = this._callbacks[i].callback;
      callback();
    }
  }

  createArguments(state?: AuthState) {
    return { useReplaceToNavigate: true, data: state };
  }

  error(message: string) {
    const result: AuthResult = {
      status: AuthenticationResultStatus.Fail,
      message
    };
    return result;
  }

  success(state?: AuthState) {
    const result: AuthResult = {
      status: AuthenticationResultStatus.Success,
      state
    };
    return result;
  }

  redirect() {
    const result: AuthResult = {
      status: AuthenticationResultStatus.Redirect
    };
    return result;
  }

  async ensureUserManagerInitialized() {
    if (this._userManager !== undefined) {
      return;
    }

    // let response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
    // if (!response.ok) {
    //     throw new Error(`Could not load settings for '${ApplicationName}'`);
    // }

    let settings: UserManagerSettings = {
      authority: 'https://localhost:44356',
      client_id: ApplicationName,
      redirect_uri: `${window.location.origin}${ApplicationPaths.LoginCallback}`,
      post_logout_redirect_uri: `${window.location.origin}${ApplicationPaths.LoginCallback}`,
      response_type: 'code',
      scope: `Web.LoginAPI openid profile`,
      automaticSilentRenew: true,
      includeIdTokenInSilentRenew: true,
      userStore: new WebStorageStateStore({
        prefix: ApplicationName
      })
    };
    this._userManager = new UserManager(settings);

    this._userManager.events.addUserSignedOut(async () => {
      await this._userManager?.removeUser();
      this.updateState(undefined);
    });

    axios.interceptors.response.use(
      response => response,
      error => {
        if (error.response.status === 401) {
          var axiosConfig = error.response.config;

          return this.signIn({ returnUrl: `${window.location.href}` }).then(result => {
            if (result.status === AuthenticationResultStatus.Success && this._user?.access_token) {
              axiosConfig.headers['Authorization'] = `Bearer ${this._user.access_token}`;
              return axios(axiosConfig);
            }
            return Promise.reject(error);
          });
        }
        return Promise.reject(error);
      }
    );
  }

  static get instance() {
    return authService;
  }
}

const authService = new AuthorizeService();

export default authService;

export const AuthenticationResultStatus = {
  Redirect: 'redirect',
  Success: 'success',
  Fail: 'fail'
};
