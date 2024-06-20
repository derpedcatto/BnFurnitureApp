import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import {
  CardCategoryASlider,
} from "../../common/components/sliders";
import { useFetchSliderProductCategories } from "./hooks/useFetchSliderProductCategories";
import { useFetchSliderProductTypes } from "./hooks/useFetchSliderProductTypes";
import styles from "./ProductsPage.module.scss";

const DetermineSliderType: React.FC = () => {
  const location = useLocation();
  const pathSegments = location.pathname.split('/').filter(Boolean);
  const categorySlug = pathSegments[pathSegments.length - 1];
  const isProductType = pathSegments.length === 3;

  const { categories, isLoading: isProductCategoriesLoading } = useFetchSliderProductCategories(categorySlug);
  const { productTypes, isLoading: isProductTypesLoading } = useFetchSliderProductTypes(categorySlug);

  if (isProductType) {
    return null;
  }

  if (categories) {
    return (
      <CardCategoryASlider
        key={categorySlug}
        isLoading={isProductCategoriesLoading}
        categories={categories}
      />
    );
  }

  if (productTypes) {
    return (
      <CardCategoryASlider
        key={categorySlug}
        isLoading={isProductTypesLoading}
        categories={productTypes}
      />
    );
  }

  return null;
};

function FetchProductCategories(categorySlug: string) {
  const { categories, isLoading } =
    useFetchSliderProductCategories(categorySlug);

  return { categories, isLoading };
}

function FetchProductTypes(categorySlug: string) {
  const { productTypes, isLoading } = useFetchSliderProductTypes(categorySlug);

  return { productTypes, isLoading };
}

const ProductsPage: React.FC = () => {
  console.log("ProductsPage render");
  return (
    <div className={styles.productsPageContainer}>
      <div className={styles.filterSideMenu}></div>
      <div className={styles.contentContainer}>
        <div className={styles.sectionSlider}>
          <DetermineSliderType />
        </div>
      </div>
    </div>
  );
};

export default ProductsPage;

/*
type SliderComponentProps = {
  productSlug: string;
};

const SliderCategoryComponent: React.FC<SliderComponentProps> = ({
  productSlug,
}) => {
  const { categories, isLoading } =
    useFetchSliderProductCategories(productSlug);

  return (
    <CardCategoryASlider
      key="category-slider"
      isLoading={isLoading}
      categories={categories}
    />
  );
};

const SliderProductTypeComponent: React.FC<SliderComponentProps> = ({
  productSlug,
}) => {
  const { productTypes, isLoading } = useFetchSliderProductTypes(productSlug);

  return (
    <CardCategoryASlider
      key="product-type-slider"
      isLoading={isLoading}
      categories={productTypes}
    />
  );
};
*/

/*
const DetermineSliderType: React.FC = () => {
  const location = useLocation();
  const pathSegments = location.pathname.split("/").filter(Boolean);

  // Assuming the slug is the last segment of the path
  const productSlug = pathSegments[pathSegments.length - 1];
  const isProductType = pathSegments.length === 3;

  // Conditional rendering based on path
  return isProductType ? (
    null
  ) : (
    <SliderCategoryComponent productSlug={productSlug} />
  );
};
*/
