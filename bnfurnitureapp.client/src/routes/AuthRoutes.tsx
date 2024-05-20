import { Route, Routes } from "react-router-dom";
import RegisterPage from "../feature/registerPage";

export function AuthRoutes() {
  return (
    <Routes>
      <Route path="/">
        <Route path="login" />
        <Route path="register" element={ <RegisterPage /> } />
        <Route path="forgotpass" />
      </Route>
    </Routes>
  );
}
