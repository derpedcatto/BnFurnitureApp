import { configureStore } from "@reduxjs/toolkit";
import counterReducer from "../feature/example/counterSlice";
import authReducer from "../common/redux/authSlice";

export const store = configureStore({
  reducer: {
    counter: counterReducer,
    auth: authReducer
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;