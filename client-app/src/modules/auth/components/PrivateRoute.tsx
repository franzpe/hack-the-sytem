import React from 'react';
import { Route, Redirect, RouteProps } from 'react-router-dom';

import { useAppSelector } from 'store/hooks/useAppSelector';
import ROUTES from 'constants/routes';
import { selectIsAuthenticated } from '../selectors';

const PrivateRoute = (props: RouteProps) => {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);

  if (isAuthenticated) {
    return <Route {...props} />;
  } else {
    return <Redirect to={ROUTES.LOGIN} />;
  }
};

export default React.memo(PrivateRoute);
