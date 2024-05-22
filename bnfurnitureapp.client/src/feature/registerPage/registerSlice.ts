import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { ApiCommandResponse } from "../../common/types/ApiResponseTypes";
import axios from "axios";

interface RegisterState {
  isLoading: boolean;
  errors: { [key: string]: string[] } | null;
  isSuccess: boolean;
}

const initialState: RegisterState = {
  isLoading: false,
  errors: null,
  isSuccess: false,
};

interface UserData {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  mobileNumber?: string;
  address?: string;
  agreeCheckbox: boolean;
}

export const registerUser = createAsyncThunk<
  ApiCommandResponse,
  UserData,
  { rejectValue: { [key: string]: string[] } }
>("register/registerUser", async (userData, { rejectWithValue }) => {
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
      .addCase(
        registerUser.rejected,
        (
          state,
          action: PayloadAction<{ [key: string]: string[] } | undefined>
        ) => {
          state.isLoading = false;
          if (action.payload) {
            state.errors = action.payload;
          } else {
            state.errors = null;
          }
        }
      );
  },
});

export default registerSlice.reducer;
