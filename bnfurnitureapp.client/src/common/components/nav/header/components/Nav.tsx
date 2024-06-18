import React, { useState } from "react";
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
import HamburgerMenu from "./HamburgerMenu";
import { dummyData } from "../dummyData";
import { useFetchCategoriesWithTypes } from "../hooks/useFetchCategoriesWithTypes";
import { LoadingSpinner } from "../../../ui";

const Nav = () => {
  const currentUser = useSelector(
    (state: RootState) => state.user.auth.currentUser
  );
  const userIsAuthenticated = currentUser !== null;
  const userNameField = `${currentUser?.firstName} ${currentUser?.lastName}`;

  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  // const {
  //   categoriesWithTypes,
  //   isLoading: isCategoriesWithTypesLoading
  // } = useFetchCategoriesWithTypes();

  return (
    <div className={styles.container}>
      <nav className={styles.navbar}>
        <div className={styles.navLeft}>
          <NavLink to="/" className={styles.logoContainer}>
            <BnLogo />
          </NavLink>
          <div className={styles.hamburgerContainer}>
            <div
              className={`${styles.iconContainer} ${styles.hamburger}`}
              onClick={toggleMenu}
            >
              <HamburgerIcon />
              <span>Меню</span>
            </div>
            {isMenuOpen && <HamburgerMenu list={dummyData} />}
          </div>
          <NavLink className={styles.navElementLabel} to="products">
            Товари
          </NavLink>
          <NavLink className={styles.navElementLabel} to="sets">
            Набори
          </NavLink>
        </div>
        <div className={styles.navRight}>
          <NavLink to={userIsAuthenticated ? "/user/cabinet" : "/auth/login"}>
            <div className={`${styles.iconContainer} ${styles.user}`}>
              <UserIcon className={styles.userIcon} />
              {userIsAuthenticated ? (
                <span>{userNameField}</span>
              ) : (
                <span>Привіт! Увійдіть в систему</span>
              )}
            </div>
          </NavLink>
          <NavLink to="/user/cart">
            <div className={styles.iconContainer}>
              <CartIcon />
            </div>
          </NavLink>
          <NavLink to={userIsAuthenticated ? "/user/cabinet" : "/auth/login"}>
            <div className={styles.iconContainer}>
              <WishlistIcon />
            </div>
          </NavLink>
        </div>
      </nav>
      <div className={styles.searchBar}>
        <SearchBar />
      </div>
    </div>
  );
};

export default Nav;
