import React from 'react';

import ComponentHome1 from './componentHome1';


import './homePage.css';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const HomePage: React.FC = () => {
  return (

    <div>
      
      <ComponentHome1 />
      <div className="homeSection2__container"> 
        <div className='homeheaders'>знайди те, що шукаеш!</div>
        <div className='homeCardrow'>
          <div className='first__homeCard'>
            <div className='homeCardFooter'>
              <div className='homeCardText'>наша нова коллекція</div>
              <div className='homeCardSvg'>
                <svg width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <path d="M26 20L18 12L18 28L26 20ZM40 20C40 22.7667 39.4747 25.3667 38.424 27.8C37.3747 30.2333 35.95 32.35 34.15 34.15C32.35 35.95 30.2333 37.3747 27.8 38.424C25.3667 39.4747 22.7667 40 20 40C17.2333 40 14.6333 39.4747 12.2 38.424C9.76667 37.3747 7.65 35.95 5.85 34.15C4.05 32.35 2.62467 30.2333 1.574 27.8C0.524666 25.3667 -7.53293e-07 22.7667 -8.74228e-07 20C-9.95163e-07 17.2333 0.524665 14.6333 1.574 12.2C2.62467 9.76667 4.05 7.65 5.85 5.85C7.65 4.05 9.76667 2.62533 12.2 1.576C14.6333 0.525333 17.2333 -7.53293e-07 20 -8.74228e-07C22.7667 -9.95163e-07 25.3667 0.525332 27.8 1.576C30.2333 2.62533 32.35 4.05 34.15 5.85C35.95 7.65 37.3747 9.76666 38.424 12.2C39.4747 14.6333 40 17.2333 40 20Z" fill="black"/>
                </svg>
              </div>
            </div>     
          </div>
          <div className='tvelve__homeCard'>
            <div className='homeCardFooter'>
              <div className='homeCardText'>наші найкращі пропозиції</div>
              <div className='homeCardSvg'>
                <svg width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M26 20L18 12L18 28L26 20ZM40 20C40 22.7667 39.4747 25.3667 38.424 27.8C37.3747 30.2333 35.95 32.35 34.15 34.15C32.35 35.95 30.2333 37.3747 27.8 38.424C25.3667 39.4747 22.7667 40 20 40C17.2333 40 14.6333 39.4747 12.2 38.424C9.76667 37.3747 7.65 35.95 5.85 34.15C4.05 32.35 2.62467 30.2333 1.574 27.8C0.524666 25.3667 -7.53293e-07 22.7667 -8.74228e-07 20C-9.95163e-07 17.2333 0.524665 14.6333 1.574 12.2C2.62467 9.76667 4.05 7.65 5.85 5.85C7.65 4.05 9.76667 2.62533 12.2 1.576C14.6333 0.525333 17.2333 -7.53293e-07 20 -8.74228e-07C22.7667 -9.95163e-07 25.3667 0.525332 27.8 1.576C30.2333 2.62533 32.35 4.05 34.15 5.85C35.95 7.65 37.3747 9.76666 38.424 12.2C39.4747 14.6333 40 17.2333 40 20Z" fill="black"/>
                </svg>
              </div>
            </div>     
          </div>
        </div>
        <div className='tree__homeCard'>
            <div className='homeCardFooter1'>
              <div className='homeCardSvg'>
                <svg width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M26 20L18 12L18 28L26 20ZM40 20C40 22.7667 39.4747 25.3667 38.424 27.8C37.3747 30.2333 35.95 32.35 34.15 34.15C32.35 35.95 30.2333 37.3747 27.8 38.424C25.3667 39.4747 22.7667 40 20 40C17.2333 40 14.6333 39.4747 12.2 38.424C9.76667 37.3747 7.65 35.95 5.85 34.15C4.05 32.35 2.62467 30.2333 1.574 27.8C0.524666 25.3667 -7.53293e-07 22.7667 -8.74228e-07 20C-9.95163e-07 17.2333 0.524665 14.6333 1.574 12.2C2.62467 9.76667 4.05 7.65 5.85 5.85C7.65 4.05 9.76667 2.62533 12.2 1.576C14.6333 0.525333 17.2333 -7.53293e-07 20 -8.74228e-07C22.7667 -9.95163e-07 25.3667 0.525332 27.8 1.576C30.2333 2.62533 32.35 4.05 34.15 5.85C35.95 7.65 37.3747 9.76666 38.424 12.2C39.4747 14.6333 40 17.2333 40 20Z" fill="black"/>
                </svg>
              </div>
            </div>     
          </div>
      </div>


      <div className="homeSection3__container"> 
        <div className='homeheaders'>рекомаендації</div>
        <div className='homeCardrow'>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
        </div>
      </div>

      <div className="homeSection4__container"> 
        <div className='homeheaders'>найкращі набори</div>
       
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору</div>
            </div>
          </div>
       
      </div>


      <div className="homeSection5__container"> 
        <div className='homeheaders'>новинки</div> 
        <div className='homeCardrow'>
          <div className='homes5-1'></div>
          <div className='homes5-2'></div>
        </div>
        <div className='homeCardrow'>
          <div className='homes5-3'></div>
          <div className='homes5-4'></div>
        </div>
      </div>

      <div className="homeSection6__container"> 
        <div className='homeheaders'>каталог</div> 
        <div className='homeCatalogrow'>
        <div className='Component19'>
          <div className='first__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first2__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first3__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first4__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first5__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first6__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        </div> 
        <div className='homeCatalogrow'>
        <div className='Component19'>
          <div className='first7__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first8__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first9__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first10__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first11__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first12__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        </div> 
        <div className='homeCatalogrow'>
        <div className='Component19'>
          <div className='first13__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first14__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first15__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first16__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first17__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        <div className='Component19'>
          <div className='first18__homeCatalog'></div>
          <div className='Component19Text'>назва набору</div>
        </div>
        </div> 
      </div>

      <div className='__container'>
        <div className='homeheaders'>ідеї для оформлення</div>  

        <div className='image-columns'>
            <div className='column'>
                <div className='image horizontal1'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical1'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg></div>            
            </div>
            <div className='column'>
                <div className='image horizontal2'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical2'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg></div>                
            </div>            
            <div className='column'>
                <div className='image horizontal3'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical3'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
            </div>
        </div>
      </div>
      <div className='__container'>
        <div className='homeheaders'>додаткові рекомендації</div>
        <div className='Frame20row'> 
        <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         </div> 
        <div className='image-columns'>
            <div className='column'>
                <div className='image horizontal1'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical1'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg></div>            
            </div>
            <div className='column'>
                <div className='image horizontal2'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical2'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg></div>                
            </div>            
            <div className='column'>
                <div className='image horizontal3'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
                <div className='image vertical3'>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white"/>
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black"/>
                  </svg>
                </div>
            </div>
        </div>
        <div className='Frame20row'> 
        <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         <div className='Frame21'><div className='Frame21Text'>назва набору</div>
         </div>
         </div> 
      </div>
      <div className="homeSection9__container"> 
        <div className='homeheaders'>важлива інформація </div>
      </div>

        

    </div>
  );
};

export default HomePage;




