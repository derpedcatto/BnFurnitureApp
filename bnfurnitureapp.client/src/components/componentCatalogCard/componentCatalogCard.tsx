import React from 'react';
import "./componentCatalogCard.css"

interface Props {
  // необхідні властивості
}

const CatalogCard: React.FC<Props> = () => {
  return (
    <div className='catalogCard'>
        <div className='catalogCardImg'></div>
        <div className='catalogCardText'>назва набору</div>    
    </div>
  );
};

export default CatalogCard;