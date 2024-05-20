import { Route, Routes } from "react-router-dom";

export function UserRoutes() {
  return (
    <Routes>
      <Route path="/">
        <Route path="wishlist" />
        <Route path="cart" />
        <Route path="cabinet" />
      </Route>
    </Routes>
  );
}
