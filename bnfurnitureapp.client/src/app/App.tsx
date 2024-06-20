import { useEffect, useRef } from "react";
import { AppDispatch } from "./store";
import { useDispatch } from "react-redux";
import {
  Route,
  Routes,
  Navigate,
  BrowserRouter,
} from "react-router-dom";
import { AuthRoutes } from "../routes/AuthRoutes";
import { UserRoutes } from "../routes/UserRoutes";
import { NavLayout } from "../common/components/nav";
import { getCurrentUser } from "../redux/auth/authThunks";
import HomePage from "../features/homePage";
import ProductsPage from "../features/productsPage";
import "./App.scss";

function App() {
  const dispatch = useDispatch<AppDispatch>();
  const hasFetchedUser = useRef(false);

  useEffect(() => {
    if (!hasFetchedUser.current) {
      dispatch(getCurrentUser());
      hasFetchedUser.current = true;
    }
  }, [dispatch]);

  return (
    <BrowserRouter>
      <Routes>
        <Route path="*" element={<Navigate replace to="/" />} />
        <Route path="/" element={<NavLayout />}>
          <Route index element={<HomePage />} />
          <Route path="/user/*" element={<UserRoutes />} />
          <Route path="/products/*" element={<ProductsPage />} />
        </Route>
        <Route path="/auth/*" element={<AuthRoutes />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

// <Route index element= {<LandingPage />}/>

// reactdev.ru/libs/redux/advanced/UsageWithReactRouter/#url_1
// youtu.be/Ul3y1LXxzdU?si=1b26b7C_q6QbVuNl

// <Link> properties - youtu.be/Ul3y1LXxzdU?si=1b26b7C_q6QbVuNl&t=1832
// replace - when clicks back button - go to the page before the page that has this attribute
//  (ex: when user logged on website and gets redirected to main page, when he clicks back button -
//   he will not get returned to login page, and instead gets redirected to page before it)
// reloadDocument - reloads the entire page
// state={} - pass state between two links without URL changes

// <NavLink> properties - youtu.be/Ul3y1LXxzdU?si=UI_NZ71Wjp8GYrvX&t=1971
// classname
// children
// style - `style={({ isActive }) => { return isActive ? { color: "red" } : {} }}`
// Changing nav text - <NavLink>`{({ isActive }) return isActive ? "Active Home" : "Home"`</NavLink>
// `end` property - only match style on specific NavLink and ignore its children

//  <Route path='/parent'>  // element={ <ParentLayout /> } - Render parent layout in all child components, youtu.be/Ul3y1LXxzdU?si=7A1Wnh0vd_mzIxCn&t=1135
//    <Route index element = { } />   - equals to '/parent'
//    <Route path='/child1' />
//  </Route>

// TODO: Implement NAV components and put them in here (footer, header)
// TODO: How to use route param with Redux? `<Route path='/login:id' />` - how to use `id`
// TODO: <Route element = { <NavBar /> }> (place all child routes here) </Route>
// TODO: Deadend navigation - https://youtu.be/Ul3y1LXxzdU?si=oqfEeKD3t0NU0RbJ&t=2158
