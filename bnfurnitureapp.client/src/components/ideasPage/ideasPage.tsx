import React from 'react';
import MainMenu from '../header/header'; 
const DesignPage: React.FC = () => {
  return (
    <div className="productsSection1">
      <MainMenu /> {/* Відображення меню */}
      <h1>ІДЕЇ</h1>
      <p>Learn more about Ideas!</p>
    </div>
  );
};

export default DesignPage;
