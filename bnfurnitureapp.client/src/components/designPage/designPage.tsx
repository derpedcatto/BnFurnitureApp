import React from 'react';
import MainMenu from '../header/header'; 
const DesignPage: React.FC = () => {
  return (
    <div className="productsSection1">
      <MainMenu /> {/* Відображення меню */}
      <h1>ДИЗАЙН</h1>
      <p>Learn more about Design!</p>
    </div>
  );
};

export default DesignPage;