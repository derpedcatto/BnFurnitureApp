import React from 'react';

const SearchComponent: React.FC = () => {
  return (
    <div className="search">
      <img src="img/search-icon.png" alt="" className="search-icon" />
      <input type="text" className="search-input" placeholder="пошук" />
    </div>
  );
};

export default SearchComponent;


