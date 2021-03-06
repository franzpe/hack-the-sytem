import React, { useEffect, useState } from 'react';

import authService, { AuthState } from './AuthorizeService';
import { AuthenticationResultStatus } from './AuthorizeService';
import { LoginActions, QueryParameterNames, ApplicationPaths } from './ApiAuthorizationConstants';

interface LoginProps {
  action: string;
}

export const Login: React.FC<LoginProps> = ({ action }) => {
  const [state, setState] = useState<AuthState>();

  useEffect(() => {
    switch (action) {
      case LoginActions.Login:
        login(getReturnUrl());
        break;
      case LoginActions.LoginCallback:
        processLoginCallback();
        break;
      case LoginActions.LoginFailed:
        const params = new URLSearchParams(window.location.search);
        const error = params.get(QueryParameterNames.Message) ?? 'undefined error';
        setState({ message: error });
        break;
      case LoginActions.Profile:
        redirectToProfile();
        break;
      case LoginActions.Register:
        redirectToRegister();
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }, []);

  const login = (returnUrl: string) => {
    const state: AuthState = { returnUrl };
    authService.signIn(state).then(result => {
      switch (result.status) {
        case AuthenticationResultStatus.Redirect:
          break;
        case AuthenticationResultStatus.Success:
          navigateToReturnUrl(returnUrl);
          break;
        case AuthenticationResultStatus.Fail:
          setState({ message: result.message });
          break;
        default:
          throw new Error(`Invalid status result ${result.status}.`);
      }
    });
  };

  const processLoginCallback = () => {
    const url = window.location.href;
    authService.completeSignIn(url).then(result => {
      switch (result.status) {
        case AuthenticationResultStatus.Redirect:
          // There should not be any redirects as the only time completeSignIn finishes
          // is when we are doing a redirect sign in flow.
          throw new Error('Should not redirect.');
        case AuthenticationResultStatus.Success:
          navigateToReturnUrl(getReturnUrl(result.state));
          break;
        case AuthenticationResultStatus.Fail:
          setState({ message: result.message });
          break;
        default:
          throw new Error(`Invalid authentication result status '${result.status}'.`);
      }
    });
  };

  const getReturnUrl = (state?: AuthState) => {
    const params = new URLSearchParams(window.location.search);
    const fromQuery = params.get(QueryParameterNames.ReturnUrl);
    if (fromQuery && !fromQuery.startsWith(`${window.location.origin}/`)) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
    }
    return (state && state.returnUrl) || fromQuery || `${window.location.origin}/`;
  };

  const redirectToRegister = () => {
    redirectToApiAuthorizationPath(
      `${ApplicationPaths.IdentityRegisterPath}?${QueryParameterNames.ReturnUrl}=${encodeURI(
        ApplicationPaths.Login
      )}`
    );
  };

  const redirectToProfile = () => {
    redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
  };

  const redirectToApiAuthorizationPath = (apiAuthorizationPath: string) => {
    const redirectUrl = `${window.location.origin}/${apiAuthorizationPath}`;
    // It's important that we do a replace here so that when the user hits the back arrow on the
    // browser they get sent back to where it was on the app instead of to an endpoint on this
    // component.
    window.location.replace(redirectUrl);
  };

  const navigateToReturnUrl = (returnUrl: string) => {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    window.location.replace(returnUrl);
  };
  if (!!state?.message) {
    return <div>{state.message}</div>;
  } else {
    switch (action) {
      case LoginActions.Login:
        return <div>Processing login</div>;
      case LoginActions.LoginCallback:
        return <div>Processing login callback</div>;
      case LoginActions.Profile:
      case LoginActions.Register:
        return <></>;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }
};
