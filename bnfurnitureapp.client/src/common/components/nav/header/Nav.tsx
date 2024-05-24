import { NavLink } from "react-router-dom";
import { RootState } from "../../../../app/store";
import { useSelector } from "react-redux";
import styles from "./Nav.module.scss";
import BnLogo from "../../../icons/BnLogo";
import HamburgerIcon from "../../../icons/HamburgerIcon";
import UserIcon from "../../../icons/UserIcon";
import CartIcon from "../../../icons/CartIcon";
import WishlistIcon from "../../../icons/WishlistIcon";
import SearchBar from "./SearchBar";

const Nav = () => {
  const isLoggedIn = useSelector((state: RootState) => state.auth.isAuthenticated);
  const userFirstName = useSelector((state: RootState) => state.auth.user?.firstName);
  const userLastName = useSelector((state: RootState) => state.auth.user?.lastName);

  return (
    <>
      <div className={styles.container}>
        <nav>
          <div className={styles['nav-leftside']}>
            <NavLink to="/">
              <div className={`${styles['svg-logo-container']}`}>
                <BnLogo />
              </div>  
            </NavLink>
            <div className={`${styles['svg-icon-container']} ${styles['hamburger']}`}>
              <HamburgerIcon />
              <p>Меню</p>
            </div>
            <NavLink to="products">Товари</NavLink>
            <NavLink to="sets">Набори</NavLink>
          </div>
          <div className={styles['nav-rightside']}>
            <NavLink to={isLoggedIn ? '/user/cabinet' : '/auth/login'}>
              <div className={`${styles['svg-icon-container']} ${styles['user']}`}>
                <UserIcon className={styles['user-icon']} />
                {isLoggedIn ? <p>{userFirstName}  {userLastName}</p> : <p>Привіт! Увійдіть в систему</p>} 
              </div>
            </NavLink>
            <NavLink to="/user/cart">
              <div className={`${styles['svg-icon-container']}`}>
                <CartIcon />
              </div>
            </NavLink>
            <NavLink to={isLoggedIn ? '/user/cabinet' : '/auth/login'}>
              <div className={`${styles['svg-icon-container']}`}>
                <WishlistIcon />
              </div>
            </NavLink>
          </div> 
        </nav>
        <div className={styles['search-bar']}>
          <SearchBar />
        </div>
      </div>
    </>
  );
}

export default Nav;