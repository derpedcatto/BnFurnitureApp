import React, { useState, FC, ReactNode } from "react";
import { NavLink } from "react-router-dom";
import { CSSTransition } from "react-transition-group";
import { ArrowRightIcon, ArrowLeftIcon } from "../../../../icons";
import {
  CategoryWithProductTypes,
  ProductType,
} from "../../../../types/api/responseDataModels";
import styles from "./HamburgerMenu.module.scss";

interface HamburgerMenuProps {
  list: CategoryWithProductTypes[];
}

interface MenuItemCategoryProps {
  category: CategoryWithProductTypes;
}

const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  const [activeMenu, setActiveMenu] = useState(styles.menuPrimary);

  const MenuItemCategory: FC<MenuItemCategoryProps> = ({
    category
  }) => {
    return (
      <a href="#" className={styles.menuItem}>
        <div className={styles.menuItemLabel}>{category.name}</div>
        <div className={styles.menuItemIconContainer}>
          <ArrowRightIcon />
        </div>
      </a>
    );
  };

  return (
    <div className={styles.hamburgerMenu}>
      <CSSTransition
        in={activeMenu === styles.menuPrimary}
        unmountOnExit
        timeout={500}
        classNames={{
          enter: styles.menuPrimaryEnter,
          enterActive: styles.menuPrimaryEnterActive,
          exit: styles.menuPrimaryExit,
          exitActive: styles.menuPrimaryExitActive
        }}
      >
        <div className={styles.menuPrimary}>
          {list
            .filter((category) => category.subCategoryList != null)
            .map((item, index) => (
              <MenuItemCategory
                key={index}
                category={item}
              />
            ))}
        </div>
      </CSSTransition>
    </div>
  );
};

export default HamburgerMenu;

/* Draft 3
const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  const [activeMenu, setActiveMenu] = useState<string>('menuPrimary');

  const MenuItemCategory: FC<MenuItemCategoryProps> = ({ category }) => {
    const hasSubCategories = category.subCategoryList && category.subCategoryList.length > 0;
    const handleClick = () => {
      setActiveMenu(category.id); // Toggles the active sub-menu
    };

    return (
      <div>
        <a href="#" className={styles.menuItem} onClick={handleClick}>
          {category.name}
        </a>
        {activeMenu === category.id && (
          <CSSTransition
            in={true}
            unmountOnExit
            timeout={500}
            classNames={{
              enter: styles.menuPrimaryEnter,
              enterActive: styles.menuPrimaryEnterActive,
              exit: styles.menuPrimaryExit,
              exitActive: styles.menuPrimaryExitActive
            }}
          >
            <div>
              {hasSubCategories ? (
                category.subCategoryList.map((subCategory) => (
                  <MenuItemCategory key={subCategory.id} category={subCategory} />
                ))
              ) : (
                category.productTypes && category.productTypes.map((productType) => (
                  <div key={productType.id} className={styles.menuItem}>
                    {productType.name}
                  </div>
                ))
              )}
            </div>
          </CSSTransition>
        )}
      </div>
    );
  };

  return (
    <div className={styles.hamburgerMenu}>
      <CSSTransition
        in={activeMenu === 'menuPrimary'}
        unmountOnExit
        timeout={500}
        classNames={{
          enter: styles.menuPrimaryEnter,
          enterActive: styles.menuPrimaryEnterActive,
          exit: styles.menuPrimaryExit,
          exitActive: styles.menuPrimaryExitActive
        }}
      >
        <div className={styles.menuPrimary}>
          {list.map((category) => (
            <MenuItemCategory key={category.id} category={category} />
          ))}
        </div>
      </CSSTransition>
    </div>
  );
};
*/

/* Draft 2 ---
const MenuItemCategory: FC<MenuItemCategoryProps> = ({ category }) => {
  const [isActive, setIsActive] = useState(false);

  const toggleSubMenu = () => setIsActive(!isActive);

  return (
    <div>
      <a href="#" className={styles.menuItem} onClick={toggleSubMenu}>
        {category.name}
      </a>
      {isActive && category.subCategoryList && (
        <div className={styles.subMenu}>
          {category.subCategoryList.map((subCategory, index) => (
            <MenuItemCategory key={index} category={subCategory} />
          ))}
        </div>
      )}
    </div>
  );
};

const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  return (
    <div className={styles.hamburgerMenu}>
      <CSSTransition
        in={true}
        unmountOnExit
        timeout={500}
        classNames={{
          enter: styles.menuPrimaryEnter,
          enterActive: styles.menuPrimaryEnterActive,
          exit: styles.menuPrimaryExit,
          exitActive: styles.menuPrimaryExitActive
        }}
      >
        <div className={styles.menuPrimary}>
          {list.map((category, index) => (
            <MenuItemCategory key={index} category={category} />
          ))}
        </div>
      </CSSTransition>
    </div>
  );
};

export default HamburgerMenu;
*/

/* Draft 1
import React, { useState, FC, ReactNode } from 'react';
import { NavLink } from 'react-router-dom';
import { CategoryWithProductTypes, ProductType } from '../../../../types/api/responseDataModels';
import styles from './HamburgerMenu.module.scss';
import ArrowRightIcon from '../../../../icons/navigation/ArrowRightIcon';

interface HamburgerMenuProps {
  list: CategoryWithProductTypes[];
}

const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  const renderCategories = (categories: CategoryWithProductTypes[]) => {
    console.log(categories);

    return categories.map(category => (
      <li key={category.id}>
        <a href="#" onClick={e => {
          e.preventDefault();
          setActiveMenu(category.slug);
        }}>
          {category.name}
        </a>
        {activeMenu === category.slug && (
          <ul>
            {category.subCategoryList ? renderCategories(category.subCategoryList) : null}
            {category.productTypes ? renderProductTypes(category.productTypes) : null}
          </ul>
        )}
      </li>
    ));
  };

  const renderProductTypes = (productTypes: ProductType[]) => {
    return productTypes.map(productType => (
      <li key={productType.id}>
        <NavLink to={`/products/${productType.slug}`}>{productType.name}</NavLink>
      </li>
    ));
  };

  const [activeMenu, setActiveMenu] = useState<string>('');

  return (
    <nav className={styles.navbar}>
      <ul className={styles['navbar-nav']}>{renderCategories(list)}</ul>
    </nav>
  );
};

export default HamburgerMenu;

*/
