import { ThunkAction, Action } from '@reduxjs/toolkit';

import configureStore from './configureStore.dev';

const store = configureStore();

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;

export default store;
