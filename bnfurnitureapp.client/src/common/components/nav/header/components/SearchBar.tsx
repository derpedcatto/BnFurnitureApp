import styles from "./SearchBar.module.scss";
import { SearchIcon } from "../../../../icons";

const SearchBar = () => {
  return (
    <div className={styles.container}>
      <div className={styles["svg-icon-container"]}>
        <SearchIcon />
      </div>
      <input type="text" placeholder="Пошук"></input>
    </div>
  );
};

export default SearchBar;
