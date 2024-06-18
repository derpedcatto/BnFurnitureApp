import { NavLink } from 'react-router-dom';
import useEmblaCarousel from "embla-carousel-react";
import styles from './CategoryNamesButtonSlider.module.scss';

export type CategoryNamesButtonSliderProps = {
  categories: Array<{
    categoryName: string;
    redirectTo: string;
  }>;
}

const CategoryNamesButtonSlider: React.FC<CategoryNamesButtonSliderProps> = ({
  categories
}) => {
  const [emblaRef] = useEmblaCarousel({
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: true,
  });

  return (
    <div className={styles.container}>
      <div className={styles["category-list"]}>
        <div className={styles["category-list-carousel"]} ref={emblaRef}>
          <div className={styles["category-list-container"]}>
            {categories.map((category, index) => (
              <div key={index} className={styles["embla-slide"]}>
                <NavLink to={category.redirectTo} className={styles['category-button']}>
                  {category.categoryName}
                </NavLink>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}

export default CategoryNamesButtonSlider;