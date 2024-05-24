import { configureStore } from "@reduxjs/toolkit";
import counterReducer from "../feature/example/counterSlice";
import authReducer from "../redux/authSlice";
import registerReducer from "../feature/registerPage/registerSlice";
import loginReducer from "../feature/loginPage/loginSlice";
import forgotPassReducer from "../feature/forgotPassPage/forgotPassSlice";

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    auth: authReducer,
    register: registerReducer,
    login: loginReducer,
    forgotPass: forgotPassReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;