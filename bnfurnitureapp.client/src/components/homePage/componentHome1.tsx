import React from 'react';
import { Link } from 'react-router-dom';
import './componentHome1.css'
import Header from '../header/header';
import SearchPage from '../searchPage/searchPage';
import ComponentZero from '../componentZero/componentZero';
import CardHome from '../componentCardHome/componentCardHome';

const ComponentHome1: React.FC = () => {
  return (
   
      <div className="homeSection1">
        <ComponentZero /> 
        <Header/>
        <SearchPage />
                
        <div className="Frame9__container">
          <div className="Frame9Text">ВСЕ ДЛЯ ДОМУ</div>
        </div>
        <div className='bb'>
          <Link to="/register">
            <button className="component12">
              <div className="component12Text">створити акаунт та почати!</div>
            </button>
            </Link>
          
            <div className="component38">
              <div className="component38Text">каталог</div>
              <svg width="20" height="12" viewBox="0 0 20 12" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M20 1.88745L10 11.8875L-4.37114e-07 1.88745L1.775 0.112451L10 8.33745L18.225 0.112451L20 1.88745Z" fill="black"/>
              </svg>
            </div>
        </div>
  
        <CardHome/>
      
      <div className='frame19'>
        <div className='frame19Text'>
           ЛОВИ МОМЕНТ | <strong>Знижки до 60% </strong>на вибрані категорії товарів!
        </div>
      </div>

    




</div>
  );
};

export default ComponentHome1;