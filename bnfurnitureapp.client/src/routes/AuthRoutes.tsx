import { Route, Routes } from "react-router-dom";
import RegisterPage from "../features/registerPage";
import LoginPage from "../features/loginPage";
import ForgotPassPage from "../features/forgotPassPage";

export function AuthRoutes() {
  return (
    <Routes>
      <Route path="/">
        <Route path="login" element={ <LoginPage /> }/>
        <Route path="register" element={ <RegisterPage /> } />
        <Route path="forgotpass" element={ <ForgotPassPage /> } />
      </Route>
    </Routes>
  );
}
