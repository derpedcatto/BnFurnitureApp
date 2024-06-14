import CardGroupDiscover from "./CardGroupDiscover";
import {
  CardCategoryASlider,
  CategoryNamesButtonSlider,
} from "../../../common/components/sliders";
import {
  DiscoverCardSectionA,
  DiscoverCardSectionB,
} from "../../../common/components/discoverSections";
import {
  DiscoverCardCategoryBLayoutA,
  ImportantInfoSectionLayout,
} from "../../../common/layouts";
import { CardCategoryB } from "../../../common/components/cards";
import {
  dummyCategories,
  dummyDiscoverCardSectionA1,
  dummyDiscoverCardCategoryBLayoutA,
  dummyDiscoverCardSectionB1,
  dummyDiscoverCardSectionB2,
} from "../dummyData";
import imageImportantInfo from "../assets/imageImportantInfo.png";

import React from "react";
import styles from "./HomePage.module.scss";
import HeroSection from "./HeroSection";
import { useFetchSliderProductCategories } from "../hooks/useFetchSliderProductCategories";
import { useFetchSliderProductTypes } from "../hooks/useFetchSliderProductTypes";

const HomePage: React.FC = () => {
  const {
    categories,
    isLoading: isCategoriesLoading
  } = useFetchSliderProductCategories();
  const {
    productTypes,
    isLoading: isProductTypesLoading
  } = useFetchSliderProductTypes();

  const discoverCards = dummyDiscoverCardCategoryBLayoutA.map((data, index) => (
    <CardCategoryB
      key={index}
      categoryName={data.categoryName}
      imageSrc={data.imageSrc}
      redirectTo={data.redirectTo}
    />
  ));

  return (
    <div className={styles.container}>
      <HeroSection />
      <div className={styles["home-body"]}>
        <div className={styles["category-section-name"]}>
          ЗНАЙДИ ТЕ, ЩО ШУКАЄШ!
        </div>
        <CardGroupDiscover />
        <div className={styles["category-section-name"]}>РЕКОМЕНДАЦІЇ</div>
        <CardCategoryASlider
          isLoading={isCategoriesLoading}
          categories={categories}
        />
        <div className={styles["category-section-name"]}>
          НАКРАЩІ ПРОПОЗИЦІЇ
        </div>
        <CardCategoryASlider isLoading={isProductTypesLoading} categories={productTypes} />
        <div className={styles["category-section-name"]}>ТЕМАТИЧНІ НАБОРИ</div>
        <DiscoverCardSectionA items={dummyDiscoverCardSectionA1} />
        <div className={styles["category-section-name"]}>КАТАЛОГ</div>
        <DiscoverCardCategoryBLayoutA children={discoverCards} />
        <div className={styles["category-section-name"]}>
          ІДЕЇ ДЛЯ ОФОРМЛЕННЯ
        </div>
        <DiscoverCardSectionB items={dummyDiscoverCardSectionB1} />
        <div className={styles["category-section-name"]}>
          ДОДАТКОВІ РЕКОМЕНДАЦІЇ
        </div>
        <CategoryNamesButtonSlider categories={dummyCategories} />
        <DiscoverCardSectionB items={dummyDiscoverCardSectionB2} />
        <CategoryNamesButtonSlider categories={dummyCategories} />
        <div className={styles["category-section-name"]}>
          ВАЖЛИВА ІНФОРМАЦІЯ
        </div>
        <ImportantInfoSectionLayout
          headingText="ВАЖЛИВА ІНФОРМАЦІЯ"
          imageSrc={imageImportantInfo}
          children={
            <>
              <p>
                Lorem ipsum dolor sit amet consectetur adipisicing elit. Dolor
                numquam pariatur dolores. Dolorum corrupti assumenda officiis
                fugiat accusantium a debitis, perspiciatis maiores, sunt, dolor
                ea dolore vel. Similique eaque necessitatibus accusamus mollitia
                laboriosam ipsa ipsum reiciendis veritatis, consectetur unde
                alias, neque laudantium ut reprehenderit quos incidunt cum,
                aspernatur beatae molestias.
              </p>
            </>
          }
        />
      </div>
    </div>
  );
};

export default HomePage;
