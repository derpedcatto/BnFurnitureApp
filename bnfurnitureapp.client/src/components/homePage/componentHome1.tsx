import React from 'react';
import Header from '../header/header'; 
import './componentHome1.css'

const ComponentHome1: React.FC = () => {
  return (

      <div className="homeSection1">

        <Header />         
        <div className="Frame9__container">
          <div className="Frame9Text">ВСЕ ДЛЯ ДОМУ</div>
        </div>
        <div className='bb__container'>
          <div className="component12">
             <div className="component12Text">створити акаунт та почати!</div>
          </div>
          <div className="component38">
            <div className="component38Text">каталог</div>
            <svg width="20" height="12" viewBox="0 0 20 12" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M20 1.88745L10 11.8875L-4.37114e-07 1.88745L1.775 0.112451L10 8.33745L18.225 0.112451L20 1.88745Z" fill="black"/>
            </svg>
          </div>
        </div>

      </div>

  );
};

export default ComponentHome1;