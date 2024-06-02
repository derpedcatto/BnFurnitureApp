import Header from "./header/Header";
import Footer from "./footer/Footer";
import { Outlet } from 'react-router-dom';
import styles from './NavLayout.module.scss';

const NavLayout = () => {
  return (
    <div className={styles['navlayout-container']}>
      <Header />
      <div className={styles['content']}>
        <Outlet />
      </div>
      <Footer />
    </div>
  );
};

export default NavLayout;