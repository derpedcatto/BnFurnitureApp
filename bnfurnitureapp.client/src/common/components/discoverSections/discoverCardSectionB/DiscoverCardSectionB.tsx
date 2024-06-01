import { NavLink } from "react-router-dom";
import { ArrowUpRightIcon } from "../../../icons";
import styles from "./DiscoverCardSectionB.module.scss";

interface DiscoverCardSectionBProps {
  items: [
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string }
  ];
}

const DiscoverCardSectionB: React.FC<DiscoverCardSectionBProps> = ({
  items,
}) => {
  return (
    <div className={styles.container}>
      {items.map((item, index) => (
        <NavLink
          key={index}
          className={styles[`card${index + 1}`]}
          to={item.link}
        >
          <div className={`${styles["svg-icon-container"]}`}>
            <ArrowUpRightIcon />
          </div>
          {item.imageSrc && <img src={item.imageSrc} />}
        </NavLink>
      ))}
    </div>
  );
};

export default DiscoverCardSectionB;
