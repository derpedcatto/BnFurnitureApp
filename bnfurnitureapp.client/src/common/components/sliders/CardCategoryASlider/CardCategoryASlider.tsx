import styles from "./CardCategoryASlider.module.scss";
import useEmblaCarousel from "embla-carousel-react";
import { CardCategoryA } from "../../cards";

interface CardCategoryASliderProps {
  categories: Array<{
    categoryName: string;
    imageSrc: string;
    redirectTo: string;
  }>;
}

const CardCategoryASlider: React.FC<CardCategoryASliderProps> = ({
  categories,
}) => {
  const [emblaRef] = useEmblaCarousel({
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: true
  });

  return (
    <div className={styles.container}>
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
    </div>
  );
};

export default CardCategoryASlider;
