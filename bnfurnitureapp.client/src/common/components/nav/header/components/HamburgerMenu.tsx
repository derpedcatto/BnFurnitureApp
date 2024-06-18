import React, { useState, FC, ReactNode } from 'react';
import { NavLink } from 'react-router-dom';
import { CategoryWithProductTypes, ProductType } from '../../../../types/api/responseDataModels';
import styles from './HamburgerMenu.module.scss';

interface HamburgerMenuProps {
  list: CategoryWithProductTypes[];
}

const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  
  return (
    <>
    </>
  );
};

export default HamburgerMenu;

/*
import React, { useState, FC, ReactNode } from 'react';
import { NavLink } from 'react-router-dom';
import { CategoryWithProductTypes, ProductType } from '../../../../types/api/responseDataModels';
import styles from './HamburgerMenu.module.scss';

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