import { NavLink } from "react-router-dom";
import styles from "./CardProductA.module.scss";

export interface CardProductAProps {
  name: string;
  categoryString: string;
  price: string;
  finalPrice: string;
  discount: number;
  imageSrc: string;
  topSticker: boolean;
  redirectTo: string;
}

const CardProductA: React.FC<CardProductAProps> = ({
  name,
  categoryString,
  price,
  finalPrice,
  discount,
  imageSrc,
  topSticker,
  redirectTo,
}) => {
  const hasDiscount = discount > 0;

  return (
    <NavLink to={redirectTo} className={styles.cardContainer}>
      <div className={styles.stickerWrapper}>
        {topSticker && <div className={styles.topSticker}>TOP</div>}
      </div>
      <div className={styles.hideOverflowContainer}>
        <div className={styles.imageContainer}>
          <img src={imageSrc} alt={name} />
        </div>
        <div className={styles.detailsContainer}>
          <div className={styles.detailsUpper}>
            <div className={styles.detailsName}>{name}</div>
            <div className={styles.detailsCategory}>{categoryString}</div>
          </div>
          <div className={styles.detailsLower}>
            {hasDiscount ? (
              <>
                <div className={styles.detailsPriceChangeContainer}>
                  <div className={styles.priceOld}>{price}</div>
                  <div className={`${styles.discount}`}>{discount}%</div>
                </div>
                <div
                  className={`${styles.detailsFinalPrice} ${styles.colorDiscount}`}
                >
                  {finalPrice}
                </div>
              </>
            ) : (
              <div className={styles.detailsFinalPrice}>{finalPrice}</div>
            )}
          </div>
        </div>
      </div>
    </NavLink>
  );
};

export default CardProductA;
