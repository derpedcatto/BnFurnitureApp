import React, { useEffect, useRef } from 'react';
import Slider, { Settings } from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import './componentCarusel.css'

//npm install slick-carousel
//npm install @types/slick-carousel --save-dev

const Carusel: React.FC = () => {
  const sliderRef = useRef<Slider>(null);

  useEffect(() => {
    if (sliderRef.current) {
      sliderRef.current.slickGoTo(0); // Початкове зміщення на перший елемент
    }
  }, []);

  const settings: Settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 6,
    slidesToScroll: 1,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 4,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
        },
      },
    ],
  };

  return (
    <div>
      <Slider ref={sliderRef} {...settings}>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 1</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 2</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 3</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 4</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 5</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 6</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 7</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 8</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 9</div>
            </div>
          </div>
        </div>
        <div>
          <div className='second__homeCard'>
            <div className='Frame20'>
              <div className='Frame20Text'>назва набору 10</div>
            </div>
          </div>
        </div>
        {/* Додайте ще 5 елементів каруселі */}
      </Slider>
    </div>
  );
};

export default Carusel;


