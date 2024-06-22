import React from "react";
import { useLocation } from "react-router-dom";
import styles from "./ProductsPage.module.scss";
import useFetchProductArticles from "./hooks/useFetchProductArticles";
import { CardProductA } from "../../common/components/cards";
import { LoadingSpinner } from "../../common/components/ui";

function useCategorySlug() {
  const location = useLocation();
  const pathSegments = location.pathname
    .split("/")
    .filter((segment) => segment); // Filter out empty segments
  const productsIndex = pathSegments.indexOf("products");

  // If "products" is found and it's not the last segment, return the subsequent path segments joined by "/"
  if (productsIndex !== -1 && productsIndex < pathSegments.length - 1) {
    const categorySlug = pathSegments.slice(productsIndex + 1).join("/");
    return categorySlug;
  }

  return "";
}

interface PaginationProps {
  total: number;
  pageSize: number;
  current: number;
  onChange: (page: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({ total, pageSize, current, onChange }) => {
  const numPages = Math.ceil(total / pageSize);

  return (
    <div className={styles.pagination}>
      {Array.from({ length: numPages }, (_, i) => i + 1).map(page => (
        <button
          key={page}
          className={current === page ? styles.activePage : styles.page}
          onClick={() => onChange(page)}
        >
          {page}
        </button>
      ))}
    </div>
  );
};

const ProductsPage: React.FC = () => {
  const location = useLocation();
  React.useEffect(() => {}, [location]);
  const [currentPage, setCurrentPage] = React.useState(1);
  const [pageSize, setPageSize] = React.useState(10);  // Default page size

  const categorySlug = useCategorySlug();
  const { articleList, totalCount, isLoading } = useFetchProductArticles(categorySlug, currentPage, pageSize);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
    setPageSize(10); 
  };

  return (
    <div className={styles.productsPageContainer}>
      <div className={styles.productsContainer}>
        {isLoading ? (
          <div className={styles.pageLoadingSpinnerContainer}>
            <div className={styles.loadingSpinnerContainer}>
              <LoadingSpinner />
            </div>
          </div>
        ) : articleList && articleList.map((item, index) => (
          <CardProductA
          key={index}
          name={item.name}
          categoryString={item.productTypeName}
          price={`${item.price.toFixed(2)}`}
          finalPrice={item.finalPrice}
          discount={item.discount}
          imageSrc={item.thumbnailImageUri}
          topSticker={false}
          redirectTo={`/article-redirect/${item.article}`}
        />
        ))}
      </div>
      <Pagination
          total={totalCount}
          pageSize={pageSize}
          current={currentPage}
          onChange={handlePageChange}
        />
    </div>
  );
};

export default ProductsPage;

/*
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
