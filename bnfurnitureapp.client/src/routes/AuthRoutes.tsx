import { Route, Routes } from "react-router-dom";
import RegisterPage from "../feature/registerPage";
import LoginPage from "../feature/loginPage";
import ForgotPassPage from "../feature/forgotPassPage";

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
