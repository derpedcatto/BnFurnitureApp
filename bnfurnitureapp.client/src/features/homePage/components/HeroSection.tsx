import { useSelector } from "react-redux";
import { RootState } from "../../../app/store";
import { NavLink } from "react-router-dom";
import useEmblaCarousel from "embla-carousel-react";
import { HeroImage } from "../../../common/components/ui";
import { CardProductA } from "../../../common/components/cards/";
import styles from "./HeroSection.module.scss";
import ButtonStyles from  "../../../common/styles/Buttons.module.scss";
import cardProductTop1 from "../assets/CardProductA/cardProductTop1.png";
import cardProductTop2 from "../assets/CardProductA/cardProductTop2.png";
import cardProductTop3 from "../assets/CardProductA/cardProductTop3.png";
import imageHero from "../assets/imageHero.png";

const HeroSection = () => {
  const emblaOptions = {
    containScroll: "trimSnaps" as const,
    slidesToScroll: "auto" as const,
    breakpoints: {
      // [`(min-width: ${globals.})`]: { active: false },
      '(min-width: (var(--bp-w-md)))': { active: false },
    },
  };
  const [emblaRef] = useEmblaCarousel(emblaOptions);
  const user = useSelector((state: RootState) => state.user.auth.currentUser);
  const userIsAuthenticated = user !== null && user !== undefined;

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
              className={`${ButtonStyles['button-black']} ${styles["nav-button"]}`}
            >
              СТВОРИТИ АККАУНТ ТА ПОЧАТИ!
            </NavLink>
          )}

          <NavLink
            to="/products"
            className={`${ButtonStyles['button-semi-transparent']} ${styles["nav-button"]}`}
          >
            КАТАЛОГ
          </NavLink>
        </div>
        <div className={styles["product-cards-promo"]}>
          <div className={styles["product-cards-carousel"]} ref={emblaRef}>
            <div className={styles["product-cards-container"]}>
              <div className={styles["embla-slide"]}>
                <CardProductA
                  productName="ПОДУШКИ"
                  productPrice="480₴"
                  productCategory="спальна кімната"
                  imageSrc={cardProductTop1}
                  top={true}
                  redirectTo="/"
                />
              </div>
              <div className={styles["embla-slide"]}>
                <CardProductA
                  productName="КОВДРА"
                  productPrice="1200₴"
                  productCategory="спальна кімната"
                  imageSrc={cardProductTop2}
                  top={true}
                  redirectTo="/"
                />
              </div>
              <div className={styles["embla-slide"]}>
                <CardProductA
                  productName="ЛІЖКО"
                  productPrice="16600₴"
                  productCategory="спальна кімната"
                  imageSrc={cardProductTop3}
                  top={true}
                  redirectTo="/"
                />
              </div>
            </div>
          </div>
          <div className={styles["product-cards-promo-text"]}>
            ЛОВИ МОМЕНТ | <b>Знижки до <b className={styles.bolder}>60%</b></b> на вибрані категорії товарів!
          </div>
        </div>
      </div>
    </>
  );
};

export default HeroSection;
