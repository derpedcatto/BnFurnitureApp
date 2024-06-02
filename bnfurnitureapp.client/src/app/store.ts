import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { authReducer, registerReducer } from "../redux/userSlice";

const rootReducer = combineReducers({
  auth: authReducer,
  register: registerReducer,
});

export const store = configureStore({
  reducer: {
    user: rootReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
