import React from "react";
import styles from "./HomePage.module.scss";
import HeroSection from "./HeroSection";
import CardGroupDiscover from "./CardGroupDiscover";
import {
  CardCategoryASlider,
  CategoryNamesButtonSlider,
} from "../../../common/components/sliders";
import {
  DiscoverCardSectionB,
  DiscoverCardCategoryBSectionA,
} from "../../../common/components/discoverSections";
import { ImportantInfoSectionLayout } from "../../../common/layouts";
import imageImportantInfo from "../assets/imageImportantInfo.png";

import { useFetchSliderProductCategories } from "../hooks/useFetchSliderProductCategories";
import { useFetchSliderProductTypes } from "../hooks/useFetchSliderProductTypes";
import { useFetchCatalogProductTypes } from "../hooks/useFetchCatalogProductTypes";
import { useFetchDiscoverCategories } from "../hooks/useFetchDiscoverCategories";
import { useFetchButtonSliderCategories } from "../hooks/useFetchButtonSliderCategories"
import { useFetchDiscoverProductTypes } from '../hooks/useFetchDiscoverProductTypes';
import { useFetchButtonSliderProductTypes } from "../hooks/useFetchButtonSliderProductTypes";
/*
import {
  dummyDiscoverCardSectionA1,
  dummyCategories,
  dummyDiscoverCardCategoryBLayoutA,
  dummyDiscoverCardSectionB1,
  dummyDiscoverCardSectionB2,
} from "../dummyData";
 */

function SliderCategoryComponent() {
  const { categories, isLoading: isCategoriesLoading } =
    useFetchSliderProductCategories();

  return (
    <CardCategoryASlider
      key="category-slider"
      isLoading={isCategoriesLoading}
      categories={categories}
    />
  );
}

function SliderProductTypeComponent() {
  const { productTypes, isLoading: isProductTypesLoading } =
    useFetchSliderProductTypes();

  return (
    <CardCategoryASlider
      key="product-type-slider"
      isLoading={isProductTypesLoading}
      categories={productTypes}
    />
  );
}

function CatalogProductTypeComponent() {
  const { catalogProductTypes, isLoading: isCatalogLoading } =
    useFetchCatalogProductTypes();

  return (
    <DiscoverCardCategoryBSectionA
      key="discover-product-types"
      isLoading={isCatalogLoading}
      categories={catalogProductTypes}
    />
  );

  /* dummy
  const discoverCards = dummyDiscoverCardCategoryBLayoutA.map((data, index) => (
    <CardCategoryB
      key={index}
      categoryName={data.categoryName}
      imageSrc={data.imageSrc}
      redirectTo={data.redirectTo}
    />
  ));
  */
}

function DiscoverCardSectionCategoriesComponent() {
  const { categories, isLoading } = useFetchDiscoverCategories();

  return (
    <DiscoverCardSectionB items={categories.items} isLoading={isLoading} />
  );

  /* dummy
    <DiscoverCardSectionB items={dummyDiscoverCardSectionB1} />
  */
}

function DiscoverCardSectionProductTypeComponentA() {
  const { productTypes, isLoading } = useFetchDiscoverProductTypes({
    pageNumber: 2,
    pageSize: 6
  });

  return (
    <DiscoverCardSectionB items={productTypes.items} isLoading={isLoading} />
  );
}

function DiscoverCardSectionProductTypeComponentB() {
  const { productTypes, isLoading } = useFetchDiscoverProductTypes({
    pageNumber: 3,
    pageSize: 6
  });

  return (
    <DiscoverCardSectionB items={productTypes.items} isLoading={isLoading} />
  );
}

function ButtonSliderCategoriesComponent() {
  const { categories } = useFetchButtonSliderCategories();

  return (
    <CategoryNamesButtonSlider categories={categories} />
  );

  /* dummy
    <CategoryNamesButtonSlider categories={dummyCategories} />
  */
}

function ButtonSliderProductTypesComponent() {
  const { productTypes } = useFetchButtonSliderProductTypes();

  return (
    <CategoryNamesButtonSlider categories={productTypes} />
  );

  /* dummy
    <CategoryNamesButtonSlider categories={dummyCategories} />
  */
}

const HomePage: React.FC = () => {
  console.log("HomePage render");

  return (
    <div className={styles.container}>
      <HeroSection />
      <div className={styles["home-body"]}>
        <div className={styles["category-section-name"]}>
          ЗНАЙДИ ТЕ, ЩО ШУКАЄШ!
        </div>
        <CardGroupDiscover />

        <div className={styles["category-section-name"]}>РЕКОМЕНДАЦІЇ</div>
        <SliderCategoryComponent />

        <div className={styles["category-section-name"]}>
          НАКРАЩІ ПРОПОЗИЦІЇ
        </div>
        <SliderProductTypeComponent />

        <div className={styles["category-section-name"]}>НОВИНКИ</div>
        <DiscoverCardSectionProductTypeComponentA />

        <div className={styles["category-section-name"]}>КАТАЛОГ</div>
        <CatalogProductTypeComponent />

        <div className={styles["category-section-name"]}>
          ІДЕЇ ДЛЯ ОФОРМЛЕННЯ
        </div>
        <DiscoverCardSectionCategoriesComponent />

        <div className={styles["category-section-name"]}>
          ДОДАТКОВІ РЕКОМЕНДАЦІЇ
        </div>
        <ButtonSliderCategoriesComponent />
        <DiscoverCardSectionProductTypeComponentB  />
        <ButtonSliderProductTypesComponent />

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
