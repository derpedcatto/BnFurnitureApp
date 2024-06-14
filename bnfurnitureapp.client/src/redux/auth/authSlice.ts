import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthState, RegisterState, ApiUserData } from "./authTypes";
import {
  loginUser,
  registerUser,
  requestResetPassword,
  getCurrentUser,
} from "./authThunks";
import { SliceRejectResponse } from "../../common/types/SliceRejectResponse";
import { ApiQueryResponse } from "../../common/types/ApiResponseTypes";

const initialAuthState: AuthState = {
  isLoading: false,
  currentUser: null,
};

const initialRegisterState: RegisterState = {
  isLoading: false,
  isSuccess: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  reducers: {
    resetAuthState(state) {
      state.isLoading = false;
      state.currentUser = null;
      state.isSuccess = undefined;
      state.errors = undefined;
    },
  },
  extraReducers: (builder) => {
    const handlePending = (state: AuthState) => {
      state.isLoading = true;
      state.isSuccess = undefined;
      state.errors = undefined;
    };

    const handleRejected = (
      state: AuthState,
      action: PayloadAction<SliceRejectResponse | undefined>
    ) => {
      state.isLoading = false;
      state.isSuccess = false;
      state.errors = action.payload?.errors;
    };

    builder
      .addCase(loginUser.pending, handlePending)
      .addCase(loginUser.fulfilled, (state) => {
        console.log("authSlice - loginUser - fulfilled");
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(loginUser.rejected, handleRejected);

    builder
      .addCase(requestResetPassword.pending, handlePending)
      .addCase(requestResetPassword.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(requestResetPassword.rejected, handleRejected);

    builder
      .addCase(getCurrentUser.pending, handlePending)
      .addCase(
        getCurrentUser.fulfilled,
        (state, action: PayloadAction<ApiQueryResponse<ApiUserData>>) => {
          state.isLoading = false;
          state.isSuccess = true;
          console.log("Action payload data:", action.payload.data);
          state.currentUser = (action.payload.data as ApiUserData).user;
          console.log("Updated state.currentUser:", state.currentUser);
        }
      )
      .addCase(getCurrentUser.rejected, handleRejected);
  },
});

const registerSlice = createSlice({
  name: "register",
  initialState: initialRegisterState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(registerUser.pending, (state) => {
        state.isLoading = true;
        state.isSuccess = false;
        state.errors = undefined;
      })
      .addCase(registerUser.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(
        registerUser.rejected,
        (
          state: RegisterState,
          action: PayloadAction<SliceRejectResponse | undefined>
        ) => {
          state.isLoading = false;
          state.isSuccess = false;
          state.errors = action.payload?.errors;
        }
      );
  },
});

export const { resetAuthState } = authSlice.actions;
export const authReducer = authSlice.reducer;
export const registerReducer = registerSlice.reducer;
