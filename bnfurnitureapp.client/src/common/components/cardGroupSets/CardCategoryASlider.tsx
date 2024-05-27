import styles from "./CardCategoryASlider.module.scss";
import CardCategoryA from "../cards/CardCategoryA";
import useEmblaCarousel from "embla-carousel-react";

interface CardCategoryASliderProps {
  sectionName: string;
  categories: Array<{
    categoryName: string;
    imageSrc: string;
    redirectTo: string;
  }>;
}

const CardCategoryASlider: React.FC<CardCategoryASliderProps> = ({
  sectionName,
  categories,
}) => {
  const emblaOptions = {
    containScroll: "trimSnaps" as const,
    slidesToScroll: "2" as const,
    slidesPerView: "5" as const,
    align: "start" as const,
  };
  const [emblaRef] = useEmblaCarousel(emblaOptions);

  return (
    <div className={styles.container}>
      <div className={styles["category-section-name"]}>{sectionName}</div>
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
