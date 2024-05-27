import PromoBar from "./PromoBar";
import Nav from "./Nav";
import styles from "./Header.module.scss";

const Header = () => {
  return (
    <>
      <header className={styles.header}>
        <PromoBar />
        <Nav />
      </header>
    </>
  );
};

export default Header;