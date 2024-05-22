import { PayloadAction, createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { ApiCommandResponse } from "../../common/types/ApiResponseTypes";
import axios from "axios";

interface LoginState {
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
  emailOrPhone: string;
  passwordL: string;
}

export const loginUser = createAsyncThunk<
  ApiCommandResponse,
  UserData,
  { rejectValue: { [key: string]: string[] } }
>("login/loginUser", async (userData, { rejectWithValue }) => {
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
      .addCase(loginUser.pending, (state) => {
        state.isLoading = true;
        state.errors = null;
        state.isSuccess = false;
      })
      .addCase(loginUser.fulfilled, (state) => {
        state.isLoading = false;
        state.isSuccess = true;
      })
      .addCase(loginUser.rejected, (state, action: PayloadAction<{ [key: string]: string[] } | undefined>) => {
        state.isLoading = false;
        if (action.payload) {
          state.errors = action.payload;
        } else {
          state.errors = null;
        }
      }
    );
  }
})

export default loginSlice.reducer;