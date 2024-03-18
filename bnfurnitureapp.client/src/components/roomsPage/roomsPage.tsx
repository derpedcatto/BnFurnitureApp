import React from 'react';
import MainMenu from '../header/header'; 
const DesignPage: React.FC = () => {
  return (
    <div className="productsSection1">
      <MainMenu /> {/* Відображення меню */}
      <h1>КІМНАТИ</h1>
      <p>Learn more about Rooms!</p>
    </div>
  );
};

export default DesignPage;
