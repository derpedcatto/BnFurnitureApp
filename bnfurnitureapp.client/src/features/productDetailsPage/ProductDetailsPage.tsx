import styles from "./ProductDetailsPage.module.scss";
import React, { useEffect, useState, useMemo } from "react";
import { NavLink, useLocation, useNavigate } from "react-router-dom";
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
  return { product, isLoading };
}

function useProductArticle() {
  const productSlug = useProductSlug();
  const { article, isLoading } = useFetchProductArticle(productSlug);
  return { article, isLoading };
}

export interface ExtractedCharacteristic {
  name: string;
  nameSlug: string;
  value: string;
}

function useExtractCharacteristics(
  product: ProductWithCharacteristics | null
): ExtractedCharacteristic[] {
  const location = useLocation();

  return useMemo(() => {
    if (product == null || !product.characteristics) {
      return [];
    }

    // Extract the last part of the URL
    const pathParts = location.pathname.split("/");
    const characteristicPart = pathParts[pathParts.length - 1];
    const segments = characteristicPart.split("-");
    segments.shift(); // remove Product slug

    // Create a map of characteristic slugs to their details
    const characteristicMap = product.characteristics.reduce(
      (acc, characteristic) => {
        // Safely add the characteristic and its values if they exist
        if (characteristic && characteristic.slug && characteristic.values) {
          acc[characteristic.slug] = {
            name: characteristic.name,
            nameSlug: characteristic.slug,
            values: characteristic.values.reduce((valAcc, value) => {
              if (value && value.value && value.slug) {
                valAcc[value.value] = value.slug;
              }
              return valAcc;
            }, {} as { [key: string]: string }),
          };
        }
        return acc;
      },
      {} as {
        [key: string]: {
          name: string;
          nameSlug: string;
          values: { [key: string]: string };
        };
      }
    );

    // Ensure characteristicMap is not empty before mapping
    if (Object.keys(characteristicMap).length === 0) {
      return [];
    }

    // Pair values with characteristic names and slugs
    const extractedCharacteristics: ExtractedCharacteristic[] = segments.map(
      (segment, index) => {
        // Check if the characteristic slug exists before accessing
        const characteristicSlug = Object.keys(characteristicMap).sort()[index];
        if (characteristicSlug && characteristicMap[characteristicSlug]) {
          const characteristic = characteristicMap[characteristicSlug];

          return {
            name: characteristic.name,
            nameSlug: characteristic.nameSlug,
            value: segment,
          };
        } else {
          // Return a placeholder if the characteristic slug is missing
          return {
            name: "",
            nameSlug: "",
            value: segment,
          };
        }
      }
    );

    console.log("extractedCharacteristics", extractedCharacteristics);

    return extractedCharacteristics;
  }, [location.pathname, product]);
}

interface ProductOptionsComponentProps {
  product: ProductWithCharacteristics;
  article: ProductArticle;
  currentCharacteristics: ExtractedCharacteristic[];
}

const ProductOptionsComponent: React.FC<ProductOptionsComponentProps> = ({
  product,
  article,
  currentCharacteristics,
}) => {
  const [selectedOptions, setSelectedOptions] = useState<{
    [key: string]: string;
  }>({});
  const [visibleMenu, setVisibleMenu] = useState<string | null>(null);

  useEffect(() => {
    const initialSelectedOptions = currentCharacteristics.reduce(
      (acc, characteristic) => {
        acc[characteristic.nameSlug] = characteristic.value;
        return acc;
      },
      {} as { [key: string]: string }
    );
    console.log("Initial Selected Options:", initialSelectedOptions);
    setSelectedOptions(initialSelectedOptions);
  }, [currentCharacteristics]);

  const handleOptionSelect = (characteristicId: string, valueId: string) => {
    setSelectedOptions((prev) => ({ ...prev, [characteristicId]: valueId }));
    console.log('selectedOptions', selectedOptions);
    setVisibleMenu(null);
  };

  const toggleMenu = (characteristicId: string) => {
    setVisibleMenu((prev) =>
      prev === characteristicId ? null : characteristicId
    );
  };

  return (
    <div className={styles.productOptionsContainer}>
      <div className={styles.sectionCharacteristicButtons}>
        {product.characteristics.map((characteristic) => (
          <div key={characteristic.id}>
            <button
              onClick={() => toggleMenu(characteristic.id)}
              className={styles.characteristicButton}
            >
              <span className={styles.characteristicButtonName}>
                Вибрати {characteristic.name}
              </span>
              <span className={styles.characteristicButtonValue}>
                {characteristic.values.find(
                  (val) => val.slug === selectedOptions[characteristic.slug]
                )?.value || characteristic.slug}
              </span>
            </button>
            {visibleMenu === characteristic.id && (
              <div className={styles.sideMenu}>
                <button
                  className={styles.closeButton}
                  onClick={() => setVisibleMenu(null)}
                >
                  Закрити
                </button>
                <label>{characteristic.name}</label>
                {characteristic.values.map((characteristicValue) => (
                  <button
                    key={characteristicValue.id}
                    onClick={() =>
                      handleOptionSelect(
                        characteristic.id,
                        characteristicValue.id
                      )
                    }
                    className={
                      selectedOptions[characteristic.slug]?.toLowerCase() ===
                      characteristicValue.slug.toLowerCase()
                        ? styles.selected
                        : ""
                    }
                    disabled={
                      selectedOptions[characteristic.slug]?.toLowerCase() ===
                      characteristicValue.slug.toLowerCase()
                    }
                  >
                    {characteristicValue.value}
                  </button>
                ))}
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};

const ProductDetailsPage: React.FC = () => {
  const { product, isLoading: productLoading } = useProduct();
  const { article, isLoading: articleLoading } = useProductArticle();
  const isLoading = productLoading || articleLoading;
  const currentCharacteristics: ExtractedCharacteristic[] =
    useExtractCharacteristics(product);

  console.log("ProductDetailsPage render");
  console.log("characteristics", currentCharacteristics);

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
        {product && article && (
          <ProductOptionsComponent
            product={product}
            article={article}
            currentCharacteristics={currentCharacteristics}
          />
        )}
      </div>
    </div>
  );
};

export default ProductDetailsPage;
