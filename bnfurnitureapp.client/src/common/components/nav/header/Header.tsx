import PromoBar from "./PromoBar";
import Nav from "./Nav";
import "./Header.module.scss";

const Header = () => {
  return (
    <>
      <header>
        <PromoBar />
        <Nav />
      </header>
    </>
  );
};

export default Header;