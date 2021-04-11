import { Dispatch } from 'redux';

import setupInterceptors from './configureInterceptors';

const configureAxios = (dispatch: Dispatch) => {
  setupInterceptors(dispatch);
};

export default configureAxios;
