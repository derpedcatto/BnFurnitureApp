import { NavLink } from "react-router-dom";
import styles from "./CardCategoryA.module.scss";

export type CardCategoryAProps = {
  categoryName: string;
  imageSrc: string;
  redirectTo: string;
}

const CardCategoryA: React.FC<CardCategoryAProps> = ({
  categoryName,
  imageSrc,
  redirectTo,
}) => {
  return (
    <NavLink to={redirectTo} className={styles.container}>
      <div className={styles["image-container"]}>
        <img src={imageSrc} />
      </div>
      <div className={styles["category-text-name"]}>{categoryName}</div>
    </NavLink>
  );
};

export default CardCategoryA;