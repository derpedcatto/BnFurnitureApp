import styles from "./CardGroupSetA.module.scss";
import image1 from "./assets/CardGroupSetA/1.png";
import image2 from "./assets/CardGroupSetA/2.png";
import image3 from "./assets/CardGroupSetA/3.png";
import ArrowRightIcon from "../../icons/ArrowRightIcon";
import { NavLink } from "react-router-dom";

const CardGroupSetA = () => {
  return (
    <>
      <div className={styles.container}>
        <div className={`${styles.card} ${styles.card1} ${styles['card-horizontal']}`}>
          <div className={styles['image-container']}>
            <img src={ image1 } />
          </div>
          <NavLink to={'/'} className={styles['card-footer']}>
            <p>НАША НОВА КОЛЛЕКЦІЯ</p>
            <div className={`${styles['svg-logo-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
        <div className={`${styles.card} ${styles.card2} ${styles['card-horizontal']}`}>
          <div className={styles['image-container']}>
            <img src={ image2 } />
          </div>
          <NavLink to={'/'} className={styles['card-footer']}>
            <p>НАШІ НАЙКРАЩІ ПРОПОЗИЦІЇ</p>
            <div className={`${styles['svg-logo-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
        <div className={`${styles.card} ${styles.card3} ${styles['card-vertical']}`}>
          <div className={styles['image-container']}>
            <img src={ image3 } />
          </div>
          <NavLink to={'/'} className={styles['card-aside']}>
            <div className={`${styles['svg-logo-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
      </div>
    </>
  );
};

export default CardGroupSetA;
