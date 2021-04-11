import { Route, Switch, useRouteMatch } from 'react-router';

import Login from './Login';

const LoginRoot = () => {
  const { path } = useRouteMatch();

  return (
    <Switch>
      <Route exact path={path}>
        <Login />
      </Route>
    </Switch>
  );
};

export default LoginRoot;
