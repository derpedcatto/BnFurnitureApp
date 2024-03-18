
import React from 'react';
import MainMenu from '../header/header'; 
const DesignPage: React.FC = () => {
  return (
    <>
    <div className="productsSection1">
      <MainMenu /> {/* Відображення меню */}
      <h1>ТОВАРИ</h1>
      <p>Learn more about Products!</p>
    </div>
    <div className="productsSection2"></div>
    </>
  );
};

export default DesignPage;

