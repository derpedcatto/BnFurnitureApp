export interface AuthState {
  isLoading: boolean;
  currentUser: UserData | null;
  isSuccess?: boolean;
  errors?: Record<string, string[]>;
}

export interface RegisterState {
  isLoading: boolean;
  isSuccess: boolean;
  errors?: Record<string, string[]>;
}

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