import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";
import {
  ApiCommandResponse,
  ApiQueryResponse,
} from "../common/types/ApiResponseTypes";
import { AppDispatch } from "../app/store";
import { getErrorMessage, logError } from "../common/utils/ErrorHandlingUtils";
import { SliceRejectResponse } from "../common/types/SliceRejectResponse";

interface AuthState {
  isLoading: boolean;
  currentUser: UserData | null;
  isSuccess?: boolean;
  errors?: Record<string, string[]>;
}

interface RegisterState {
  isLoading: boolean;
  isSuccess: boolean;
  errors?: Record<string, string[]>;
}

const initialAuthState: AuthState = {
  isLoading: false,
  currentUser: null,
};

const initialRegisterState: RegisterState = {
  isLoading: false,
  isSuccess: false,
};

export interface UserRegisterProps {
  email: string;
  password: string;
  repeatPassword: string;
  firstName: string;
  lastName: string;
  mobileNumber: string | null;
  address: string | null;
  agreeCheckbox: boolean;
}

export interface UserLoginProps {
  emailOrPhone: string;
  password: string;
}

export interface UserForgotPassProps {
  emailOrPhone: string;
}

export interface ApiUserData {
  user: UserData;
}

export interface UserData {
  id: string;
  email: string;
  phoneNumber: string | null;
  firstName: string;
  lastName: string;
  address: string | null;
  registeredAt: Date;
  lastLoginAt: Date | null;
}

export const registerUser = createAsyncThunk<
  ApiCommandResponse,
  UserRegisterProps,
  { rejectValue: SliceRejectResponse }
>("auth/registerUser", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/signup",
      userData
    );
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      } as SliceRejectResponse);
    }
    return response.data;
  } catch (error: unknown) {
    const errorMessage = getErrorMessage(error);
    let errorResponse: SliceRejectResponse = { message: errorMessage };
    logError("registerUser", errorMessage, error);

    if (error instanceof AxiosError && error.response?.data) {
      const responseData = error.response.data as ApiCommandResponse;
      errorResponse = {
        message: `${responseData.statusCode}: ${responseData.message}`,
        errors: responseData.errors,
      };
    }
    return rejectWithValue(errorResponse);
  }
});

export const loginUser = createAsyncThunk<
  ApiCommandResponse,
  UserLoginProps,
  { dispatch: AppDispatch; rejectValue: SliceRejectResponse }
>("auth/loginUser", async (userData, { dispatch, rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/login",
      userData
    );
    console.log("eggs");

    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      } as SliceRejectResponse);
    }

    await dispatch(getCurrentUser());
    return response.data;
  } catch (error: unknown) {
    const errorMessage = getErrorMessage(error);
    let errorResponse: SliceRejectResponse = { message: errorMessage };
    logError("loginUser", errorMessage, error);

    if (error instanceof AxiosError && error.response?.data) {
      const responseData = error.response.data as ApiCommandResponse;
      errorResponse = {
        message: `${responseData.statusCode}: ${responseData.message}`,
        errors: responseData.errors,
      };
    }
    return rejectWithValue(errorResponse);
  }
});

export const requestResetPassword = createAsyncThunk<
  ApiCommandResponse,
  UserForgotPassProps,
  { rejectValue: SliceRejectResponse }
>("auth/requestResetPassword", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/passforgot",
      userData
    );
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      } as SliceRejectResponse);
    }
    return response.data;
  } catch (error: unknown) {
    const errorMessage = getErrorMessage(error);
    let errorResponse: SliceRejectResponse = { message: errorMessage };
    logError("requestResetPassword", errorMessage, error);

    if (error instanceof AxiosError && error.response?.data) {
      const responseData = error.response.data as ApiCommandResponse;
      errorResponse = {
        message: `${responseData.statusCode}: ${responseData.message}`,
        errors: responseData.errors,
      };
    }
    return rejectWithValue(errorResponse);
  }
});

export const getCurrentUser = createAsyncThunk<
  ApiQueryResponse<ApiUserData>,
  void,
  { rejectValue: SliceRejectResponse }
>("auth/getCurrentUser", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get<ApiQueryResponse<ApiUserData>>(
      "api/user/current-user"
    );
    console.log("bumps");
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      } as SliceRejectResponse);
    }
    return response.data;
  } catch (error: unknown) {
    const errorMessage = getErrorMessage(error);
    let errorResponse: SliceRejectResponse = { message: errorMessage };
    logError("getCurrentUser", errorMessage, error);

    if (error instanceof AxiosError && error.response?.data) {
      const responseData = error.response.data as ApiQueryResponse<UserData>;
      errorResponse = {
        message: `${responseData.statusCode}: ${responseData.message}`,
        errors: responseData.errors,
      };
    }
    return rejectWithValue(errorResponse);
  }
});

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

    // Login
    builder
      .addCase(loginUser.pending, handlePending)
      .addCase(loginUser.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(loginUser.rejected, handleRejected);

    // Reset pass
    builder
      .addCase(requestResetPassword.pending, handlePending)
      .addCase(requestResetPassword.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(requestResetPassword.rejected, handleRejected);

    // Get current user
    builder
      .addCase(getCurrentUser.pending, handlePending)
      .addCase(
        getCurrentUser.fulfilled,
        (state, action: PayloadAction<ApiQueryResponse<ApiUserData>>) => {
          state.isLoading = false;
          state.isSuccess = true;
          state.currentUser = (action.payload.data as ApiUserData).user;
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
