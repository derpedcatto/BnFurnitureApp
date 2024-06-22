import useEmblaCarousel from "embla-carousel-react";
import styles from "./CardCategoryASlider.module.scss";
import { CardCategoryA } from "../../cards";
import { LoadingSpinner } from "../../ui";
import { CardCategoryAProps } from "../../cards";

export interface CardCategoryASliderProps {
  isLoading: boolean;
  categories: Array<CardCategoryAProps>;
}

const CardCategoryASlider: React.FC<CardCategoryASliderProps> = ({
  categories,
  isLoading,
}) => {
  const [emblaRef] = useEmblaCarousel({
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: true,
  });

  return (
    <div className={styles.container}>
      {isLoading ? (
        <div className={styles['loading-icon-container']}>
          <LoadingSpinner />
        </div>
      ) : (
        <div className={styles["category-cards"]}>
          <div className={styles["category-cards-carousel"]} ref={emblaRef}>
            <div className={styles["category-cards-container"]}>
              {categories.map((category, index) => (
                <div key={index} className={styles["embla-slide"]}>
                  <CardCategoryA
                    categoryName={category.categoryName}
                    imageSrc={category.imageSrc}
                    redirectTo={category.redirectTo}
                  />
                </div>
              ))}
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default CardCategoryASlider;
