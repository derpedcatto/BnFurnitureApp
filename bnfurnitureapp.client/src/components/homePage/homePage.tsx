import React from 'react';

import ComponentHome1 from './componentHome1';
import Carusel from '../componentCarusel/componentCarusel';
import NewReleases from '../componentNewReleases/componentNewReleases';
import CatalogCard from '../componentCatalogCard/componentCatalogCard'


import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import './homePage.css';
import ImageColumns from '../componentImage6/componentImage6';
import TextRow from '../componentTextRow/componentTextRow';
import ImportantInfo from '../componentImportantInfo/ImportantInfo';
import NewCollection from '../componentNewCollection/componentNewCollection';


const HomePage: React.FC = () => {
  return (

    <div>
<ComponentHome1 />


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

