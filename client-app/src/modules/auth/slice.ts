import { createSlice } from '@reduxjs/toolkit';

interface AuthState {
  isAuthenticated: boolean;
  isAuthenticating: boolean;
}

const initialState: AuthState = {
  isAuthenticated: false,
  isAuthenticating: false
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {},
  extraReducers: builder => {}
});

export const authActions = { ...authSlice.actions };

export default authSlice.reducer;
