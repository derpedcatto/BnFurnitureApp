import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { AppDispatch } from "../../app/store";
import {
  ApiCommandResponse,
  ApiQueryResponse,
} from "../../common/types/api/ApiResponseTypes";
import { SliceRejectResponse } from "../../common/types/SliceRejectResponse";
import { handleApiError } from "../../common/utils/ErrorHandlingUtils";
import {
  UserRegisterProps,
  UserLoginProps,
  UserForgotPassProps,
  ApiUserData,
} from "./authTypes";

export const registerUser = createAsyncThunk<
  ApiCommandResponse,
  UserRegisterProps,
  { rejectValue: SliceRejectResponse }
>("auth/registerUser", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "user/signup",
      userData
    );
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      });
    }
    return response.data;
  } catch (error) {
    return rejectWithValue(handleApiError(error, "registerUser"));
  }
});

export const loginUser = createAsyncThunk<
  ApiCommandResponse,
  UserLoginProps,
  { dispatch: AppDispatch; rejectValue: SliceRejectResponse }
>("auth/loginUser", async (userData, { dispatch, rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "user/login",
      userData
    );
    console.log("authThunk - loginUser", response.data);
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      });
    }

    await dispatch(getCurrentUser());
    return response.data;
  } catch (error) {
    return rejectWithValue(handleApiError(error, "loginUser"));
  }
});

export const requestResetPassword = createAsyncThunk<
  ApiCommandResponse,
  UserForgotPassProps,
  { rejectValue: SliceRejectResponse }
>("auth/requestResetPassword", async (userData, { rejectWithValue }) => {
  try {
    const response = await axios.post<ApiCommandResponse>(
      "user/passforgot",
      userData
    );
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      });
    }
    return response.data;
  } catch (error) {
    return rejectWithValue(handleApiError(error, "requestResetPassword"));
  }
});

export const getCurrentUser = createAsyncThunk<
  ApiQueryResponse<ApiUserData>,
  void,
  { rejectValue: SliceRejectResponse }
>("auth/getCurrentUser", async (_, { rejectWithValue }) => {
  try {
    const response = await axios.get<ApiQueryResponse<ApiUserData>>(
      "user/current"
    );
    console.log("authThunk - getCurrentUser", response.data);
    if (!response.data.isSuccess) {
      return rejectWithValue({
        errors: response.data.errors,
        message: `${response.data.statusCode}: ${response.data.message}`,
      });
    }
    return response.data;
  } catch (error) {
    return rejectWithValue(handleApiError(error, "getCurrentUser"));
  }
});
