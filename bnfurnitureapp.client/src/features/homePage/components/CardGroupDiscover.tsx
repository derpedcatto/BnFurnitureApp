import styles from "./CardGroupDiscover.module.scss";
import setImage1 from "../assets/CardGroupDiscover/1.png";
import setImage2 from "../assets/CardGroupDiscover/2.png";
import setImage3 from "../assets/CardGroupDiscover/3.png";
import ArrowRightIcon from "../../../common/icons/navigation/ArrowRightIcon";
import { NavLink } from "react-router-dom";

const CardGroupSetA = () => {
  return (
    <>
      <div className={styles.container}>
        <div className={`${styles.card} ${styles.card1} ${styles['card-horizontal']}`}>
          <div className={styles['image-container']}>
            <img src={ setImage1 } />
          </div>
          <NavLink to={'/'} className={styles['card-footer']}>
            <p>НАША НОВА КОЛЛЕКЦІЯ</p>
            <div className={`${styles['svg-icon-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
        <div className={`${styles.card} ${styles.card2} ${styles['card-horizontal']}`}>
          <div className={styles['image-container']}>
            <img src={ setImage2 } />
          </div>
          <NavLink to={'/'} className={styles['card-footer']}>
            <p>НАШІ НАЙКРАЩІ ПРОПОЗИЦІЇ</p>
            <div className={`${styles['svg-icon-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
        <div className={`${styles.card} ${styles.card3} ${styles['card-vertical']}`}>
          <div className={styles['image-container']}>
            <img src={ setImage3 } />
          </div>
          <NavLink to={'/'} className={styles['card-aside']}>
            <div className={`${styles['svg-icon-container']}`}>
              <ArrowRightIcon />
            </div>
          </NavLink>
        </div>
      </div>
    </>
  );
};

export default CardGroupSetA;
