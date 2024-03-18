import React from 'react';
import ComponentHome1 from './componentHome1';

import './homePage.css';

const HomePage: React.FC = () => {
  return (

    <div>
      
      <ComponentHome1 />
      <div className="homeSection2__container"> 
        <div className='homeheaders'>знайди те, що шукаеш!</div>
      </div>
      <div className="homeSection3__container"> 
        <div className='homeheaders'>рекомаендації</div>
      </div>
      <div className="homeSection4__container"> 
        <div className='homeheaders'>найкращі набори</div>
      </div>
      <div className="homeSection5__container"> 
        <div className='homeheaders'>новинки</div>  
      </div>
      <div className="homeSection6__container"> 
        <div className='homeheaders'>каталог</div>  
      </div>
      <div className="homeSection7__container"> 
        <div className='homeheaders'>ідеї для оформлення</div>  
      </div>
      <div className="homeSection8__container"> 
        <div className='homeheaders'>додаткові рекомендації</div>
      </div>
      <div className="homeSection9__container"> 
        <div className='homeheaders'>важлива інформація</div>
      </div>

        

    </div>
  );
};

export default HomePage;




