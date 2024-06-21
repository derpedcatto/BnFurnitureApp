import React from "react";
import { useLocation } from "react-router-dom";
import styles from "./ProductsPage.module.scss";

const ProductsPage: React.FC = () => {
  const location = useLocation();
  React.useEffect(() => {}, [location]);

  console.log("ProductsPage render");

  return (
    <div className={styles.productsPageContainer}>
      <div className={styles.filterSideContainer}>
        <span>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Asperiores
          quod iusto ut possimus laudantium quae, porro reiciendis consequatur
          perferendis id tenetur nam ullam eveniet alias molestias sequi dolor
          vitae provident architecto minus quasi obcaecati velit ea! Amet eaque
          perspiciatis, id architecto natus dolorem consectetur molestiae at
          explicabo iusto porro neque.
        </span>
      </div>
      <div className={styles.productsContainer}>
      <span>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Asperiores
          quod iusto ut possimus laudantium quae, porro reiciendis consequatur
          perferendis id tenetur nam ullam eveniet alias molestias sequi dolor
          vitae provident architecto minus quasi obcaecati velit ea! Amet eaque
          perspiciatis, id architecto natus dolorem consectetur molestiae at
          explicabo iusto porro neque.
        </span>
      </div>
    </div>
  );
};

export default ProductsPage;

/*

*/

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
