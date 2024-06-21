import styles from "./ProductDetailsPage.module.scss";
import React, { useEffect, useState } from "react";
import { NavLink, useLocation } from "react-router-dom";
import { useFetchProduct } from "./hooks/useFetchProduct";
import { useFetchProductArticle } from "./hooks/useFetchProductArticle";
import {
  ProductArticle,
  ProductWithCharacteristics,
} from "../../common/types/api/responseDataModels";
import { LoadingSpinner } from "../../common/components/ui";
import { ImageSlider } from "./components/ImageSlider";

function useProductSlug() {
  const location = useLocation();
  const productSlug = location.pathname.split("/").pop() ?? "";
  return productSlug;
}

function useProduct() {
  const productSlug = useProductSlug();
  const { product, isLoading } = useFetchProduct(productSlug);
  console.log("useProduct", product);
  return { product, isLoading };
}

function useProductArticle() {
  const productSlug = useProductSlug();
  const { article, isLoading } = useFetchProductArticle(productSlug);
  console.log("useProductArticle", article);
  return { article, isLoading };
}

const ProductOptionsComponent: React.FC<{
  product: ProductWithCharacteristics;
}> = ({ product }) => {
  return (
    <>
      <div>{product.name}</div>
    </>
  );
};

const ProductDetailsPage: React.FC = () => {
  const { product, isLoading: productLoading } = useProduct();
  const { article, isLoading: articleLoading } = useProductArticle();
  const isLoading = productLoading || articleLoading;

  console.log("ProductDetailsPage render");

  return isLoading ? (
    <div className={styles.loadingSpinnerPageContainer}>
      <div className={styles.loadingSpinnerContainer}>
        <LoadingSpinner />
      </div>
    </div>
  ) : (
    <div className={styles.productDetailsContainer}>
      <div className={styles.productShowcaseContainer}>
        {article && <ImageSlider imageUris={article.galleryImages} />}
        {product && <ProductOptionsComponent product={product} />}
      </div>
    </div>
  );
};

export default ProductDetailsPage;

/*
const ProductDetailsPage: React.FC = () => {
  const location = useLocation();

  console.log('ProductDetailsPage render')

  const getProductSlug = () => {
    return location.pathname.split('/').pop() ?? '';
  }

  const useFetchData = () => {
    const productSlug = getProductSlug();
    const { product, isLoading: productLoading } = useFetchProduct(productSlug);
    const { article, isLoading: articleLoading } = useFetchProductArticle(productSlug);

    useEffect(() => {
      console.log(product);
      console.log(article);
    }, [product, article]);

    return { product, article, isLoading: productLoading || articleLoading };
  };

  const { product, article, isLoading } = useFetchData();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div className={styles.productDetailsContainer}>
      <NavLink to="/product-details/bedroom_dressers_malm-white-4">
        Malm White 4 Drawer Dresser
      </NavLink>
      <NavLink to="/product-details/bedroom_dressers_malm-white-6">
        Malm White 6 Drawer Dresser
      </NavLink>
      {product && <div>{product.name}</div>}
      {article && <div>{article.name}</div>}
    </div>
  );
}

*/

/*
function GetProductSlug() {
  const location = useLocation();
  const productSlug = location.pathname.split('/').pop() ?? '';

  return productSlug;
}

function GetProduct() {
  const productSlug = GetProductSlug();

  const { product, isLoading } =
    useFetchProduct(productSlug);

  console.log('GetProduct', product);

  return { product, isLoading }
}

function GetProductArticle() {
  const productSlug = GetProductSlug();

  const { article, isLoading } =
    useFetchProductArticle(productSlug);

  console.log('GetProductArticle', article);

  return { article, isLoading };
}

const ProductDetailsPage: React.FC = () => {
  GetProduct();
  GetProductArticle();

  return (
    <div className={styles.productDetailsContainer}>
      <NavLink reloadDocument={true} to="product-details/bedroom_dressers_malm-white-4">
        Malm White 4 Drawer Dresser
      </NavLink>
      <NavLink reloadDocument={true} to="product-details/bedroom_dressers_malm-white-6">
        Malm White 6 Drawer Dresser
      </NavLink>
    </div>
  );
}
*/
