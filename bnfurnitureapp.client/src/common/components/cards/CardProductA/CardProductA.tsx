import { NavLink } from "react-router-dom";
import styles from "./CardProductA.module.scss";

interface CardProductAProps {
  productName: string;
  productCategory: string;
  productPrice: string;
  imageSrc: string;
  top: boolean;
  redirectTo: string;
}

const CardProductA: React.FC<CardProductAProps> = ({
  productName,
  productCategory,
  productPrice,
  imageSrc,
  top,
  redirectTo,
}) => {
  return (
    <NavLink to={redirectTo} className={styles.container}>
      {top == true ? <div className={styles["top-sticker"]}>TOP</div> : <></>}
      <div className={styles["hide-overflow-container"]}>
        <div className={styles["image-container"]}>
          <img src={imageSrc} />
        </div>
        <div className={styles["details-container"]}>
          <div className={styles["details-name"]}>{productName}</div>
          <div className={styles["details-category"]}>{productCategory}</div>
          <div className={styles["details-price"]}>{productPrice}</div>
        </div>
      </div>
    </NavLink>
  );
};

export default CardProductA;
