import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { ApiCommandResponse } from "../../common/types/ApiResponseTypes";
import axios from "axios";

interface RegisterState {
  isLoading: boolean;
  errors: Record<string, string[]> | null;
  isSuccess: boolean;
}

const initialState: RegisterState = {
  isLoading: false,
  errors: null,
  isSuccess: false,
};

interface UserFormData {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  mobileNumber?: string;
  address?: string;
  agreeCheckbox: boolean;
}

export const registerUser = createAsyncThunk<ApiCommandResponse, UserFormData, { rejectValue: Record<string, string[]> }>
  ("auth/registerUser", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/signup",
      userData,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    console.log("Server Response:", response.data);
    return response.data.isSuccess ? response.data : rejectWithValue(response.data.errors);
  } catch (error: any) {
    console.error("Server Error:", error.response.data);
    return rejectWithValue(error.response.data.errors);
  }
});

const registerSlice = createSlice({
  name: "register",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(registerUser.pending, (state) => {
        state.isLoading = true;
        state.errors = null;
        state.isSuccess = false;
      })
      .addCase(registerUser.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(registerUser.rejected, (state, action) => {
        state.isLoading = false;
        state.errors = action.payload || null;
      });
  },
});


export default registerSlice.reducer;
