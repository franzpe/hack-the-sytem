import React, { useEffect } from 'react';
//import authService, { AuthState } from './AuthorizeService';
//import { AuthenticationResultStatus } from './AuthorizeService';
//import { LogoutActions, QueryParameterNames, ApplicationPaths } from './ApiAuthorizationConstants';

interface LogoutProps {
  action: string;
}

export const Logout: React.FC<LogoutProps> = ({ action }) => {
  //const [state, setState] = useState<AuthState>();

  useEffect(() => {}, [action]);

  return <></>;
};
