import { NavLink } from "react-router-dom";
import { RootState } from "../../../../../app/store";
import { useSelector } from "react-redux";
import styles from "./Nav.module.scss";
import SearchBar from "./SearchBar";
import {
  BnLogo,
  HamburgerIcon,
  UserIcon,
  CartIcon,
  WishlistIcon,
} from "../../../../icons";

const Nav = () => {
  const currentUser = useSelector((state: RootState) => state.user.auth.currentUser);
  const userIsAuthenticated = currentUser !== null;
  const userNameField = `${currentUser?.firstName} ${currentUser?.lastName}`;

  return (
    <div className={styles.container}>
      <nav>
        <div className={styles["nav-leftside"]}>
          <NavLink to="/">
            <div className={`${styles["svg-logo-container"]}`}>
              <BnLogo />
            </div>
          </NavLink>
          <div
            className={`${styles["svg-icon-container"]} ${styles["hamburger"]}`}
          >
            <HamburgerIcon />
            <p>Меню</p>
          </div>
          <NavLink to="products">Товари</NavLink>
          <NavLink to="sets">Набори</NavLink>
        </div>
        <div className={styles["nav-rightside"]}>
          <NavLink to={userIsAuthenticated ? "/user/cabinet" : "/auth/login"}>
            <div
              className={`${styles["svg-icon-container"]} ${styles["user"]}`}
            >
              <UserIcon className={styles["user-icon"]} />
              {userIsAuthenticated ? (
                <p>{userNameField}</p>
              ) : (
                <p>Привіт! Увійдіть в систему</p>
              )}
            </div>
          </NavLink>
          <NavLink to="/user/cart">
            <div className={`${styles["svg-icon-container"]}`}>
              <CartIcon />
            </div>
          </NavLink>
          <NavLink to={userIsAuthenticated ? "/user/cabinet" : "/auth/login"}>
            <div className={`${styles["svg-icon-container"]}`}>
              <WishlistIcon />
            </div>
          </NavLink>
        </div>
      </nav>
      <div className={styles["search-bar"]}>
        <SearchBar />
      </div>
    </div>
  );
};

export default Nav;
