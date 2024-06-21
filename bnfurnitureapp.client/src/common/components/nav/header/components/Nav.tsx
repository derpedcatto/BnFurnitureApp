import React, { useState, useEffect, useCallback, useRef } from "react";
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
import { HamburgerMenuProps } from "./HamburgerMenu";
import HamburgerMenu from "./HamburgerMenu";
import { useFetchCategoriesWithTypes } from "../hooks/useFetchCategoriesWithTypes";
import { LoadingSpinner } from "../../../ui";
import { dummyData } from "../dummyData";

const getBreakpoint = () => {
  const rootStyle = getComputedStyle(document.documentElement);
  const breakpointValue = rootStyle.getPropertyValue("--bp-w-sm").trim();
  return parseInt(breakpointValue, 10);
};

const BREAKPOINT = getBreakpoint();

const HamburgerMenuComponent: React.FC<HamburgerMenuProps> = React.memo(({ list }) => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const menuRef = useRef<HTMLDivElement>(null);

  const handleResize = useCallback(() => {
    if (window.innerWidth >= BREAKPOINT && isMenuOpen) {
      setIsMenuOpen(false);
      document.body.classList.remove(styles.noScroll);
    }
  }, [isMenuOpen]);

  useEffect(() => {
    window.addEventListener("resize", handleResize);
    return () => window.removeEventListener("resize", handleResize);
  }, [handleResize]);

  const scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
  };

  const toggleMenu = () => {
    const isSmallScreen = window.innerWidth < BREAKPOINT;
    setIsMenuOpen((prev) => {
      const nextMenuState = !prev;
      if (nextMenuState && isSmallScreen) {
        scrollToTop();
        document.body.classList.add(styles.noScroll);
      } else {
        document.body.classList.remove(styles.noScroll);
      }
      return nextMenuState;
    });
  };

    /*
  const handleClickOutside = (event: MouseEvent) => {
    if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
      setIsMenuOpen(false);
      document.body.classList.remove(styles.noScroll);
    }
  };

  useEffect(() => {
    if (isMenuOpen) {
      document.addEventListener('click', handleClickOutside);
    } else {
      document.removeEventListener('click', handleClickOutside);
    }
    return () => document.removeEventListener('click', handleClickOutside);
  }, [isMenuOpen]);
  */

  return (
    <div className={styles.hamburgerMenuContainer} ref={menuRef}>
      <div
        className={`${styles.iconContainer} ${styles.hamburgerIcon}`}
        onClick={toggleMenu}
      >
        <HamburgerIcon />
        <span>Меню</span>
      </div>
      {isMenuOpen && <HamburgerMenu list={list} />}
    </div>
  );
});

const Nav = React.memo(() => {
  const currentUser = useSelector(
    (state: RootState) => state.user.auth.currentUser
  );
  const userIsAuthenticated = currentUser !== null;
  const userNameField = `${currentUser?.firstName} ${currentUser?.lastName}`;

  // const { categoriesWithTypes, isLoading: isCategoriesWithTypesLoading } =
  //   useFetchCategoriesWithTypes();

  return (
    <div className={styles.container}>
      <nav className={styles.navbar}>
        <div className={styles.navLeft}>
          <NavLink to="/" className={styles.logoContainer}>
            <BnLogo />
          </NavLink>
          {/*isCategoriesWithTypesLoading ? (
            <div className={styles.loadingIconContainer}>
              <LoadingSpinner />
            </div>
          ) : (
            <HamburgerMenuComponent list={dummyData} />
          )*/}
          <HamburgerMenuComponent list={dummyData} />
          <NavLink className={styles.navElementLabel} to="products">
            Товари
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
});

export default Nav;

/*
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

  console.log('Nav render');

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
*/
