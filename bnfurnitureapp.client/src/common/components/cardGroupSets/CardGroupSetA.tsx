import styles from "./CardGroupSetA.module.scss";
import image1 from "../assets/CardGroupSetA/1.png";
import image2 from "../assets/CardGroupSetA/2.png";
import image3 from "../assets/CardGroupSetA/3.png";

const CardGroupSetA = () => {
  return (
    <>
      <div className={styles.container}>
        <div className={`${styles.card} ${styles.card1} ${styles['card-horizontal']}`}>
          <div className={styles['img-wrapper']}>
            <img src={ image1 } />
          </div>
          <div className={styles['card-footer']}>
            <p>НАША НОВА КОЛЛЕКЦІЯ</p>
          </div>
        </div>
        <div className={`${styles.card} ${styles.card2} ${styles['card-horizontal']}`}>
          <div className={styles['img-wrapper']}>
            <img src={ image2 } />
          </div>
          <div className={styles['card-footer']}>
            <p>НАШІ НАЙКРАЩІ ПРОПОЗИЦІЇ</p>
          </div>
        </div>
        <div className={`${styles.card} ${styles.card3} ${styles['card-vertical']}`}>
          <div className={styles['img-wrapper']}>
            <img src={ image3 } />
          </div>
          <div className={styles['card-aside']}>
            <p>aaa</p>
          </div>
        </div>
      </div>
    </>
  );
};

export default CardGroupSetA;
