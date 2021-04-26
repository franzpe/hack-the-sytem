import reduxImmutableStateInvariant from 'redux-immutable-state-invariant';
import { configureStore as createStore } from '@reduxjs/toolkit';

import { rootReducer } from './reducer';

const devMiddlewares = [reduxImmutableStateInvariant()];

export default function configureStore() {
  return createStore({
    reducer: rootReducer,
    middleware: getDefaultMiddleware => {
      const defMiddleware = getDefaultMiddleware();

      if (process.env.NODE_ENV === 'development') {
        defMiddleware.concat(...devMiddlewares);
      }

      return defMiddleware;
    },
    devTools: process.env.NODE_ENV === 'development'
  });
}
