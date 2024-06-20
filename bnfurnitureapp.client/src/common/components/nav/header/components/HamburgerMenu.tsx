import React, { useState, useCallback, FC, useRef, useEffect } from "react";
import { NavLink } from "react-router-dom";
import { CSSTransition } from "react-transition-group";
import {
  CategoryWithProductTypes,
  ProductType,
} from "../../../../types/api/responseDataModels";
import styles from "./HamburgerMenu.module.scss";

export interface HamburgerMenuProps {
  list: CategoryWithProductTypes[];
}

interface MenuItemsProps {
  subCategoryList?: CategoryWithProductTypes[] | null;
  productTypes?: ProductType[] | null;
  parentSlug: string;
  onClick: (item: CategoryWithProductTypes | ProductType) => void;
}

const isCategoryWithProductTypes = (
  item: CategoryWithProductTypes | ProductType
): item is CategoryWithProductTypes => {
  return (item as CategoryWithProductTypes).subCategories !== undefined;
};

const HamburgerMenu: FC<HamburgerMenuProps> = ({ list }) => {
  const [menuStack, setMenuStack] = useState<CategoryWithProductTypes[]>([]);
  const [currentMenuIndex, setCurrentMenuIndex] = useState(0);
  const [menuHeight, setMenuHeight] = useState<number | undefined>(0);
  const [transitionDirection, setTransitionDirection] = useState<
    "Left" | "Right" | "None"
  >("None");

  // Using a ref to store references to the menu elements
  const menuRefs = useRef<React.RefObject<HTMLDivElement>[]>([]);
  const menuContainerRef = useRef<HTMLDivElement>(null);

  const calculateHeight = useCallback(() => {
    if (menuContainerRef.current) {
      const activeMenu = menuRefs.current[currentMenuIndex]?.current;
      if (activeMenu) {
        setMenuHeight(activeMenu.scrollHeight);
      }
    }
  }, [currentMenuIndex, menuContainerRef]);

  useEffect(() => {
    calculateHeight();
  }, [menuStack, currentMenuIndex, calculateHeight]);

  // Initialize menuStack with the initial list on component mount or when list changes
  useEffect(() => {
    setMenuStack([
      {
        id: "",
        name: "",
        slug: "",
        priority: null,
        cardImageUri: "",
        thumbnailImageUri: "",
        subCategories: list ?? null,
        productTypes: null,
      },
    ]);
  }, [list]);

  // Update refs array whenever the menuStack changes
  useEffect(() => {
    menuRefs.current = menuStack.map(() => React.createRef<HTMLDivElement>());
    setCurrentMenuIndex(menuStack.length - 1);
  }, [menuStack]);

  const handleMenuItemClick = useCallback(
    (item: CategoryWithProductTypes | ProductType) => {
      if (isCategoryWithProductTypes(item)) {
        setTransitionDirection("Left");
        setMenuStack((prev) => [...prev, item]);
      } else {
        // ProductType click
      }
    },
    []
  );

  const handleBackClick = useCallback(() => {
    setTransitionDirection("Right");
    setMenuStack((prev) => prev.slice(0, -1));
  }, []);

  return (
    <div
      className={styles.hamburgerMenu}
      ref={menuContainerRef}
      style={{ height: `${menuHeight}px` }}
    >
      {menuStack.map((menu, index) => (
        <CSSTransition
          key={index}
          in={index === currentMenuIndex}
          timeout={300}
          classNames={{
            enter: styles[`menuSlideEnter${transitionDirection}`],
            enterActive: styles[`menuSlideEnterActive${transitionDirection}`],
            exit: styles[`menuSlideExit${transitionDirection}`],
            exitActive: styles[`menuSlideExitActive${transitionDirection}`],
          }}
          nodeRef={menuRefs.current[index]}
          unmountOnExit
          onExited={() => setCurrentMenuIndex(menuStack.length - 1)}
        >
          <div ref={menuRefs.current[index]} className={styles.menuContainer}>
            {index > 0 && (
              <a
                href="#"
                className={styles.menuBackButton}
                onClick={handleBackClick}
              >
                <span>Назад</span>
              </a>
            )}
            <MenuItems
              subCategoryList={menu.subCategories}
              productTypes={menu.productTypes}
              parentSlug={menu.slug}
              onClick={handleMenuItemClick}
            />
          </div>
        </CSSTransition>
      ))}
    </div>
  );
};

const MenuItems: FC<MenuItemsProps> = ({
  subCategoryList,
  productTypes,
  parentSlug,
  onClick,
}) => (
  <>
    <NavLink to={`products/${parentSlug}`} className={styles.menuItem}>
      Переглянути усі
    </NavLink>
    {subCategoryList?.map((subCategory, idx) => (
      <NavLink
        to={`#`} // products/${subCategory.slug}
        key={`subcategory-${subCategory.slug}-${idx}`}
        className={styles.menuItem}
        onClick={() => onClick(subCategory)}
      >
        {subCategory.name}
      </NavLink>
    ))}
    {productTypes?.map((productType, idx) => (
      <NavLink
        to={`products/${parentSlug}/${productType.slug}`}
        key={`productType-${productType.categorySlug}-${productType.slug}-${idx}`}
        className={styles.menuItem}
        onClick={() => onClick(productType)}
      >
        {productType.name}
      </NavLink>
    ))}
  </>
);

export default HamburgerMenu;
