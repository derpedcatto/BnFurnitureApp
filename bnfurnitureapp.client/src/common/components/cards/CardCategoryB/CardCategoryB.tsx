import { NavLink } from 'react-router-dom';
import styles from './CardCategoryB.module.scss';

interface CardCategoryBProps {
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
    <NavLink to={redirectTo} className={styles.container}>
      <div className={styles['image-container']}>
        <img src={imageSrc} />
      </div>
      <div className={styles['category-name']}>{categoryName}</div>
    </NavLink>
  );
};

export default CardCategoryB;