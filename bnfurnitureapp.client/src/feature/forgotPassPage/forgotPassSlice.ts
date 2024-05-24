import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { ApiCommandResponse } from "../../common/types/ApiResponseTypes";

interface ForgotPassState {
  isLoading: boolean;
  errors: Record<string, string[]> | null;
  isSuccess: boolean;
}

const initialState: ForgotPassState = {
  isLoading: false,
  errors: null,
  isSuccess: false,
};

interface UserData {
  emailOrPhone: string;
}

export const requestResetPassword = createAsyncThunk<ApiCommandResponse, UserData, { rejectValue: Record<string, string[]> }>
  ("auth/requestResetPassword", async (UserData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/passforgot",
      UserData,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    console.log("Server Response:", response.data);
    if (!response.data.isSuccess) {
      console.log("Validation Errors:", response.data.errors);
      return rejectWithValue(response.data.errors);
    }
    return response.data;
  } catch (error: any) {
    console.error("Server Error:", error.response.data);
    return rejectWithValue(error.response.data.errors);
  }
});

const forgotPassSlice = createSlice({
  name: "forgotPass",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(requestResetPassword.pending, (state) => {
        state.isLoading = true;
        state.errors = null;
        state.isSuccess = false;
      })
      .addCase(requestResetPassword.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(requestResetPassword.rejected, (state, action) => {
        state.isLoading = false;
        state.errors = action.payload || null;
      });
  },
});

export default forgotPassSlice.reducer;
