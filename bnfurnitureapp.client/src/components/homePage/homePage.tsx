import React from 'react';
import { Link } from 'react-router-dom';

import Carusel from '../componentCarusel/componentCarusel';
import NewReleases from '../componentNewReleases/componentNewReleases';
import CatalogCard from '../componentCatalogCard/componentCatalogCard'
import ImageColumns from '../componentImage6/componentImage6';
import TextRow from '../componentTextRow/componentTextRow';
import ImportantInfo from '../componentImportantInfo/ImportantInfo';
import NewCollection from '../componentNewCollection/componentNewCollection';
import Header from '../header/header';
import SearchPage from '../searchPage/searchPage';
import ComponentZero from '../componentZero/componentZero';
import CardHome from '../componentCardHome/componentCardHome';

import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import './homePage.css';


const HomePage: React.FC = () => {
  return (

    <div>

<div className="homeSection1">
        <ComponentZero /> 
        <Header/>
        <SearchPage />
                
        <div className="Frame9">
          <div className="Frame9Text">ВСЕ ДЛЯ ДОМУ</div>
        </div>
        <div className='bb'>
          <Link to="/register">
            <button className="component12">
              <div className="component12Text">створити акаунт та почати!</div>
            </button>
            </Link>
          
            <button className="component38">
              <div className="component38Text">каталог</div>
              <svg width="20" height="12" viewBox="0 0 20 12" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M20 1.88745L10 11.8875L-4.37114e-07 1.88745L1.775 0.112451L10 8.33745L18.225 0.112451L20 1.88745Z" fill="black"/>
              </svg>
            </button>
        </div>
  
        <CardHome/>
      
      <div className='frame19'>
        <div className='frame19Text'>
           ЛОВИ МОМЕНТ | <strong>Знижки до 60% </strong>на вибрані категорії товарів!
        </div>
      </div>

    




</div>

<div className="__container"> 
  <div className='homeheaders'>знайди те, що шукаеш!</div>
  <NewCollection/>
</div>


<div className="__container"> 
  <div className='homeheaders'>рекомаендації</div>
  <Carusel/>
</div>

<div className="__container"> 
  <div className='homeheaders'>найкращі набори</div>
  <Carusel/>
</div>

<div className="__container"> 
  <div className='homeheaders'>новинки</div> 
  <NewReleases/>
</div>

<div className="__container"> 
  <div className='homeheaders'>каталог</div> 
  <div className='homeCatalogrow'>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
  </div> 
  <div className='homeCatalogrow'>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
  </div> 
  <div className='homeCatalogrow'>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
    <CatalogCard/>
  </div> 
</div>

<div className='__container'>
  <div className='homeheaders'>ідеї для оформлення</div>  
  <ImageColumns/>
</div>

<div className='__container'>
  <div className='homeheaders'>додаткові рекомендації</div>
   <TextRow/>
   <ImageColumns/>
   <TextRow/>
</div>
        
<div className="__container"> 
  <div className='homeheaders'>важлива інформація </div>
  <ImportantInfo/>
</div>
</div>
  );
};

export default HomePage;

