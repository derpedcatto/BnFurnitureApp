import { useSelector } from "react-redux";
import { RootState } from "../../../app/store";
import { NavLink } from "react-router-dom";
import { HeroImage } from "../../../common/components/ui";
import styles from "./HeroSection.module.scss";
import ButtonStyles from "../../../common/styles/Buttons.module.scss";
import cardProductTop1 from "../assets/CardProductA/cardProductTop1.png";
import cardProductTop2 from "../assets/CardProductA/cardProductTop2.png";
import cardProductTop3 from "../assets/CardProductA/cardProductTop3.png";
import imageHero from "../assets/imageHero.png";
import { CardProductAProps } from "../../../common/components/cards/";
import { CardProductASlider } from "../../../common/components/sliders";

const HeroSection = () => {
  const user = useSelector((state: RootState) => state.user.auth.currentUser);
  const userIsAuthenticated = user !== null && user !== undefined;

  const products: CardProductAProps[] = [
    {
      name: "VESTMARKA",
      price: "480₴",
      finalPrice: "480₴",
      discount: 0,
      categoryString: "Безпружинні матраси",
      imageSrc: `${cardProductTop1}`,
      topSticker: true,
      redirectTo: "/",
    },
    {
      name: "IDASEN",
      price: "1200₴",
      finalPrice: "840₴",
      discount: 30,
      categoryString: "Столи з регульованою висотою",
      imageSrc: `${cardProductTop2}`,
      topSticker: true,
      redirectTo: "/",
    },
    {
      name: "FRIDHULT",
      price: "1600₴",
      finalPrice: "640₴",
      discount: 60,
      categoryString: "Софи-ліжка",
      imageSrc: `${cardProductTop3}`,
      topSticker: true,
      redirectTo: "/",
    },
  ];

  return (
    <>
      <HeroImage imageSrc={imageHero} />
      <div className={styles.container}>
        <div className={styles["hero-lead"]}>ВСЕ ДЛЯ ДОМУ</div>
        <div className={styles["nav-button-container"]}>
          {userIsAuthenticated ? (
            <></>
          ) : (
            <NavLink
              to="/auth/register"
              className={`${ButtonStyles["button-black"]} ${styles["nav-button"]}`}
            >
              СТВОРИТИ АККАУНТ ТА ПОЧАТИ!
            </NavLink>
          )}

          <NavLink
            to="/products"
            className={`${ButtonStyles["button-semi-transparent"]} ${styles["nav-button"]}`}
          >
            КАТАЛОГ
          </NavLink>
        </div>
        <div className={styles["product-cards-promo"]}>
          <CardProductASlider products={products} height={300} width={210} />
          <div className={styles["product-cards-promo-text"]}>
            ЛОВИ МОМЕНТ |{" "}
            <b>
              Знижки до <b className={styles.bolder}>60%</b>
            </b>{" "}
            на вибрані категорії товарів!
          </div>
        </div>
      </div>
    </>
  );
};

export default HeroSection;
