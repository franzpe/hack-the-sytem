import reduxImmutableStateInvariant from 'redux-immutable-state-invariant';
import thunk from 'redux-thunk';
import { configureStore as createStore } from '@reduxjs/toolkit';

import { rootReducer } from './reducer';

const middleware = [thunk, reduxImmutableStateInvariant()];

export default function configureStore() {
  return createStore({ reducer: rootReducer, middleware, devTools: true });
}
