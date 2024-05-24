import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ApiCommandResponse } from "../../common/types/ApiResponseTypes";
import axios from "axios";
import { login } from "../../redux/authSlice";

interface LoginState {
  isLoading: boolean;
  errors: Record<string, string[]> | null;
  isSuccess: boolean;
}

const initialState: LoginState = {
  isLoading: false,
  errors: null,
  isSuccess: false,
};

interface UserData {
  emailOrPhone: string;
  password: string;
}

export const loginUser = createAsyncThunk<ApiCommandResponse, UserData, { rejectValue: Record<string, string[]> | null }>
  ("auth/loginUser", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "api/user/login",
      userData,
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

const loginSlice = createSlice({
  name: "login",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loginUser.pending, state => {
        state.isLoading = true;
        state.errors = null;
        state.isSuccess = false;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        state.isLoading = false;
        state.isSuccess = true;
        // if (action.payload.data) {
        //   action.asyncDispatch(login(action.payload.data))
        // }
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.isLoading = false;
        state.errors = action.payload || null;
      });
  },
});

export default loginSlice.reducer;
