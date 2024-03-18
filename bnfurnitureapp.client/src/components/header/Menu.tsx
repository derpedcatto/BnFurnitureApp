import React from 'react';


interface MenuButtonProps {
  onClick: () => void;
}

const MenuButton: React.FC<MenuButtonProps> = ({ onClick }) => {
  return (
    <div className="menu" onClick={onClick}>
      <div className="hamburger-menu">
        <div className="hamburger-menu__bar"></div>
        <div className="hamburger-menu__bar"></div>
        <div className="hamburger-menu__bar"></div>        
      </div>
      <div className="menu-text">Меню</div>
    </div>
  );
}

export default MenuButton;


