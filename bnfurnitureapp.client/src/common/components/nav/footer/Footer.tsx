import { NavLink } from "react-router-dom";
import { InstaIcon, TelegramIcon, TwitterIcon } from "../../../icons/socials";
import {
  ApplePayLogo,
  MaestroLogo,
  MastercardLogo,
  PaypalLogo,
  VisaLogo,
} from "../../../icons/payment";
import styles from "./Footer.module.scss";
import { ArrowUpIcon } from "../../../icons";

const Footer = () => {
  const scrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  return (
    <footer className={styles.footer}>
      <div className={styles.container}>
        <nav className={styles["nav-row-links"]}>
        <div
          onClick={scrollToTop}
          className={`${styles["svg-icon-container"]} ${styles["button-scroll-to-top"]}`}
        >
          <ArrowUpIcon />
        </div>
          <nav className={styles["nav-socials"]}>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <InstaIcon />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <TwitterIcon />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <TelegramIcon />
            </NavLink>
          </nav>
          <nav className={styles["nav-payoptions"]}>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <VisaLogo />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <MastercardLogo />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <MaestroLogo />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <PaypalLogo />
            </NavLink>
            <NavLink to={"/"} className={`${styles["svg-icon-container"]}`}>
              <ApplePayLogo />
            </NavLink>
          </nav>
        </nav>
      </div>
    </footer>
  );
};

export default Footer;
