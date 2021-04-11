import { configureStore as createStore } from '@reduxjs/toolkit';
import thunk from 'redux-thunk';

import { rootReducer } from './reducer';

const middleware = [thunk];

export default function configureStore() {
  return createStore({ reducer: rootReducer, middleware, devTools: true });
}
