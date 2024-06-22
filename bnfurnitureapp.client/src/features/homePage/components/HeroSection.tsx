import { useSelector } from "react-redux";
import { RootState } from "../../../app/store";
import { NavLink } from "react-router-dom";
import { HeroImage } from "../../../common/components/ui";
import styles from "./HeroSection.module.scss";
import ButtonStyles from "../../../common/styles/Buttons.module.scss";
import imageHero from "../assets/imageHero.png";
import { CardProductAProps } from "../../../common/components/cards/";
import { CardProductASlider } from "../../../common/components/sliders";
import useFetchHeroSectionArticles from "../hooks/useFetchHeroSectionArticles";
import { LoadingSpinner } from "../../../common/components/ui";

const HeroSection = () => {
  const user = useSelector((state: RootState) => state.user.auth.currentUser);
  const userIsAuthenticated = user !== null && user !== undefined;

  const articleSlugs = [
    "bedroom_dressers_malm-white-6",
    "bedroom_springless_mattresses_vagstranda-firmlight_blue-80x200",
    "bedroom_linen_blankets_dytag-dark_blue-130x170",
  ];

  const { article: article1, isLoading: load1, productSlug: slug1 } = useFetchHeroSectionArticles(
    articleSlugs[0]
  );
  const { article: article2, isLoading: load2, productSlug: slug2 } = useFetchHeroSectionArticles(
    articleSlugs[1]
  );
  const { article: article3, isLoading: load3, productSlug: slug3 } = useFetchHeroSectionArticles(
    articleSlugs[2]
  );

  const articleList = [
    { article: article1, productSlug: slug1 },
    { article: article2, productSlug: slug2 },
    { article: article3, productSlug: slug3 },
  ];

  const validArticles = articleList.filter(({ article }) => article !== null);

  const products: CardProductAProps[] = validArticles.map(({ article, productSlug }) => ({
    name: article!.name,
    categoryString: article!.productTypeName, 
    price: `${article!.price}`,
    finalPrice: `${article!.finalPrice}`,
    discount: article!.discount, 
    imageSrc: article!.thumbnailImageUri,
    topSticker: true,
    redirectTo: `product-details/${productSlug}`,
  }));

  const isLoading = load1 || load2 || load3;

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
          {isLoading ? (
            <div className={styles.loadingSpinnerContainer}>
              <LoadingSpinner />
            </div>
          ) : (
            <CardProductASlider products={products} height={300} width={210} />
          )}

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

/*
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
*/
