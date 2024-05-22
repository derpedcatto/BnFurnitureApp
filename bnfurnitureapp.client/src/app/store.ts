import { configureStore } from "@reduxjs/toolkit";
import counterReducer from "../feature/example/counterSlice";
import authReducer from "../common/redux/authSlice";
import registerReducer from "../feature/registerPage/registerSlice";
import loginReducer from "../feature/loginPage/loginSlice";

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    auth: authReducer,
    register: registerReducer,
    login: loginReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;