import {
  createSlice,
  PayloadAction,
  createAsyncThunk,
  isRejectedWithValue,
} from "@reduxjs/toolkit";
import axios from "axios";
import Cookies from "js-cookie";
import { ApiCommandResponse, ApiQueryResponse } from "../common/types/ApiResponseTypes";

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

interface AuthState {
  user: UserData | null;
  isAuthenticated: boolean;
}

const initialState: AuthState = {
  user: null,
  isAuthenticated: !!Cookies.get("AuthUserId"),
}

export const fetchUser = createAsyncThunk<ApiQueryResponse<UserData>, { userId: string }, { rejectValue: Record<string, string[]> | null }>
  ("auth/fetchUser", async ({ userId }, { rejectWithValue }) => {
  try {
    const response = await axios.get<ApiQueryResponse<UserData>>(
      `api/user/${userId}`
    );
    console.log("Server Response:", response.data);
    if (!response.data.isSuccess) {
      console.log("Fetch Error:", response.data.errors);
      return rejectWithValue(response.data.errors);
    }
    return response.data;
  } catch (error: any) {
    console.error("Server Error: ", error.response.data);
    return rejectWithValue(error.response.data.errors);
  }
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action: PayloadAction<UserData>) => {  // pass only id
      state.user = action.payload;
      state.isAuthenticated = true;
      // Cookies.set("AuthUserId", action.payload.id);
    },
    logout: (state) => {
      state.user = null;
      state.isAuthenticated = false;
      Cookies.remove("AuthUserId");
    },
    setUser: (state, action: PayloadAction<UserData | null>) => {
      state.user = action.payload;
      state.isAuthenticated = action.payload !== null;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchUser.fulfilled, (state, action) => {
      state.user = action.payload.data;
      state.isAuthenticated = true;
      // Cookies.set("AuthUserId", action.payload.data.id);
    });
  }
});

export const { login, logout, setUser } = authSlice.actions;

export default authSlice.reducer;