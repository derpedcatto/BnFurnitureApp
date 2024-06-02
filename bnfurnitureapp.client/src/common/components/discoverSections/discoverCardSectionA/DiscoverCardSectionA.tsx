import { NavLink } from "react-router-dom";
import styles from "./DiscoverCardSectionA.module.scss";

type DiscoverCardSectionAProps = {
  items: [
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string },
    { imageSrc: string; link: string }
  ];
}

const DiscoverCardSectionA: React.FC<DiscoverCardSectionAProps> = ({
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
          {item.imageSrc && <img src={item.imageSrc} />}
        </NavLink>
      ))}
    </div>
  );
};

export default DiscoverCardSectionA;
