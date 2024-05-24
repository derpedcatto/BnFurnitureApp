import React, { useEffect, useState } from 'react';
import './componentCardSlider.css'; // Замініть на ваш файл зі стилями

const CardSlider: React.FC = () => {
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('data1.json');
        const jsonData = await response.json();
        setData(jsonData.data);
      } catch (error) {
        console.error('Failed to fetch data: ', error);
      }
    };

    fetchData();
  }, []);
  return (
    <div>
      {data.map((item, index) => (
        <div key={index} className='secondCard1' style={{ backgroundImage: `url(${item.url})` }}>
          <div className='Frame20'>
            <div className='Frame20Text'>{item.category}</div>
          </div>
        </div>
      ))}
    </div>
  );
  
};

export default CardSlider;





