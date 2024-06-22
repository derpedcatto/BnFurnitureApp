import { Route, Routes } from "react-router-dom";
import CartPage from "../features/cartPage";

export function UserRoutes() {
  return (
    <Routes>
      <Route path="/">
        <Route path="wishlist" />
        <Route path="cart" element={<CartPage />} />
        <Route path="cabinet" />
      </Route>
    </Routes>
  );
}
