import {
  createSlice,
  createAsyncThunk,
  // PayloadAction,
  // isRejectedWithValue,
} from "@reduxjs/toolkit";
import axios from "axios";
import { ApiCommandResponse, ApiQueryResponse } from "../common/types/ApiResponseTypes";
import { AppDispatch, RootState } from "../app/store";

interface AuthState {
  isLoading: boolean;
  errors: Record<string, string[]> | null;
  isSuccess: boolean;
  currentUser?: UserData;
}

const initialState: AuthState = {
  isLoading: false,
  errors: null,
  isSuccess: false,
};

interface UserData {
  id: string;
  email: string;
  phoneNumber?: string;
  firstName: string;
  lastName: string;
  address?: string;
  registeredAt: Date;
  lastLoginAt?: Date;
}

interface UserRegisterData {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  mobileNumber?: string;
  address?: string;
  agreeCheckbox: boolean;
}

interface UserLoginData {
  emailOrPhone: string;
  password: string;
}

interface UserForgotPassData {
  emailOrPhone: string;
}

export const registerUser = createAsyncThunk<ApiCommandResponse, UserRegisterData, { rejectValue: Record<string, string[]> }>(
  "auth/registerUser",
  async (userData, { rejectWithValue }) => {
    try {
      const response = await axios.post<ApiCommandResponse>("api/user/signup", userData);
      console.log("registerUser Server Response:", response.data);
      return response.data.isSuccess ? response.data : rejectWithValue(response.data.errors);
    } catch (error: any) {
      console.error("registerUser Server Error:", error.response.data);
      return rejectWithValue(error.response.data.errors);
    }
  }
);

export const loginUser = createAsyncThunk<
  ApiCommandResponse,
  UserLoginData, 
  { dispatch: AppDispatch; rejectValue: Record<string, string[]> }
>(
  "auth/loginUser",
  async (userData, { dispatch, rejectWithValue }) => {
    try {
      const response = await axios.post<ApiCommandResponse>("api/user/login", userData);
      console.log("loginUser Server Response:", response.data);
      if (!response.data.isSuccess) {
        console.log("loginUser Validation Errors:", response.data.errors);
        return rejectWithValue(response.data.errors);
      }
      await dispatch(getCurrentUser());
      return response.data;
    } catch (error: any) {
      console.error("loginUser Server Error:", error.response.data);
      return rejectWithValue(error.response.data.errors);
    }
  }
);

export const requestResetPassword = createAsyncThunk<ApiCommandResponse, UserForgotPassData, { rejectValue: Record<string, string[]> }>(
  "auth/requestResetPassword",
  async (userData, { rejectWithValue }) => {
    try {
      const response = await axios.post<ApiCommandResponse>("api/user/passforgot", userData);
      console.log("requestResetPassword Server Response:", response.data);
      if (!response.data.isSuccess) {
        console.log("requestResetPassword Validation Errors:", response.data.errors);
        return rejectWithValue(response.data.errors);
      }
      return response.data;
    } catch (error: any) {
      console.error("requestResetPassword Server Error:", error.response.data);
      return rejectWithValue(error.response.data.errors);
    }
  }
);

export const getCurrentUser = createAsyncThunk<ApiQueryResponse<UserData>, void, { rejectValue: Record<string, string[]> | null }>
  ("auth/getCurrentUser", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get<ApiQueryResponse<UserData>>(
      `api/user/current-user`
    );
    console.log("getCurrentUser Server Response:", response.data);
    if (!response.data.isSuccess) {
      console.log("getCurrentUser Fetch Error:", response.data.errors);
      return rejectWithValue(response.data.errors);
    }
    return response.data;
  } catch (error: any) {
    console.error("getCurrentUser Server Error: ", error.response.data);
    return rejectWithValue(error.response.data.errors);
  }
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    resetAuthState(state) {
      state.isLoading = false;
      state.errors = null;
      state.isSuccess = false;
      state.currentUser = undefined;
    },
  },
  extraReducers: (builder) => {
    // Register
    builder.addCase(registerUser.pending, state => {
      state.isLoading = true;
      state.errors = null;
      state.isSuccess = false;
    }).addCase(registerUser.fulfilled, (state) => {
      // console.log('registerUser.fulfilled', action.payload);
      state.isLoading = false;
      state.isSuccess = true;
    }).addCase(registerUser.rejected, (state, action) => {
      state.isLoading = false;
      state.errors = action.payload || null;
    });

    // Login user
    builder.addCase(loginUser.pending, state => {
      state.isLoading = true;
      state.errors = null;
      state.isSuccess = false;
    }).addCase(loginUser.fulfilled, (state) => {
      state.isLoading = false;
      state.isSuccess = true;
    }).addCase(loginUser.rejected, (state, action) => {
      state.isLoading = false;
      state.errors = action.payload || null;
    });

    // Reset password
    builder.addCase(requestResetPassword.pending, state => {
      state.isLoading = true;
      state.errors = null;
      state.isSuccess = false;
    }).addCase(requestResetPassword.fulfilled, (state) => {
      state.isLoading = false;
      state.isSuccess = true;
    }).addCase(requestResetPassword.rejected, (state, action) => {
      state.isLoading = false;
      state.errors = action.payload || null;
    });

    // Get current user
    builder.addCase(getCurrentUser.pending, (state) => {
      state.isLoading = true;
      state.errors = null;
      state.isSuccess = false;
    }).addCase(getCurrentUser.fulfilled, (state, action) => {
      state.isLoading = false;
      state.isSuccess = true;
      state.currentUser = action.payload.data.user;
    }).addCase(getCurrentUser.rejected, (state, action) => {
      state.isLoading = false;
      state.errors = action.payload || null;
      state.currentUser = undefined;
    });
  }
});

export default authSlice.reducer;