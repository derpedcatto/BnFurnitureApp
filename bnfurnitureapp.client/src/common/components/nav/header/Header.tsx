import PromoBar from "./components/PromoBar";
import Nav from "./components/Nav";
import styles from "./Header.module.scss";

const Header = () => {
  return (
    <header className={styles.header}>
      <PromoBar />
      <Nav />
    </header>
  );
};

export default Header;
