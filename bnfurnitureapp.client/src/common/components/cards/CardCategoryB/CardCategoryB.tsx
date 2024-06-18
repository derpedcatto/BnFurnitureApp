import { NavLink } from "react-router-dom";
import styles from "./CardCategoryB.module.scss";

export interface CardCategoryBProps {
  categoryName: string;
  imageSrc: string;
  redirectTo: string;
}

const CardCategoryB: React.FC<CardCategoryBProps> = ({
  categoryName,
  imageSrc,
  redirectTo,
}) => {
  return (
    <NavLink to={redirectTo} className={styles.cardContainer}>
      <div className={styles.imageContainer}>
        <img src={imageSrc} alt={categoryName} />
      </div>
      <div className={styles.categoryNameContainer}>
        <div className={styles.categoryName}>{categoryName}</div>
      </div>
    </NavLink>
  );
};

export default CardCategoryB;
