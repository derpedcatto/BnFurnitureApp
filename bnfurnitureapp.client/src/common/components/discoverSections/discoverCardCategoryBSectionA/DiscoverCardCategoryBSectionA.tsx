import styles from "./DiscoverCardCategoryBSectionA.module.scss";
import { CardCategoryBProps } from "../../cards";
import { LoadingSpinner } from "../../ui";
import { CardCategoryB } from "../../cards";

interface DiscoverCardCategoryBSectionAProps {
  isLoading: boolean;
  categories: Array<CardCategoryBProps>;
}

const DiscoverCardCategoryBSectionA: React.FC<
  DiscoverCardCategoryBSectionAProps
> = ({ isLoading, categories }) => {
  return (
    <div className={styles.container}>
      {isLoading ? (
        <div className={styles.loadingSpinnerContainer}>
          <LoadingSpinner />
        </div>
      ) : (
        categories.map((category, index) => (
          <CardCategoryB
            key={index}
            categoryName={category.categoryName}
            imageSrc={category.imageSrc}
            redirectTo={category.redirectTo}
          />
        ))
      )}
    </div>
  );
};

export default DiscoverCardCategoryBSectionA;
