import { ApplicationPaths } from 'components/api-authorization/ApiAuthorizationConstants';
import ApiAuthorizationRoutes from 'components/api-authorization/ApiAuthorizationRoutes';
import ROUTES from 'constants/routes';
import { Layout } from 'layouts';
import { Suspense } from 'react';
import { Route, Switch } from 'react-router';

import LoginRoot from './login';

const Routes = () => {
  return (
    <Suspense fallback={<div />}>
      <Switch>
        <Route path={ROUTES.LOGIN}>
          <LoginRoot />
        </Route>
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
        <Layout>
          <Route path={ROUTES.DASHBOARD}>
            <div>Dashboard</div>
          </Route>
        </Layout>
      </Switch>
    </Suspense>
  );
};

export default Routes;
