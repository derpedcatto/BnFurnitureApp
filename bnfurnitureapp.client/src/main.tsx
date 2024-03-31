import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import HomePage from './components/homePage/homePage';
import ProductsPage from './components/productsPage/productsPage';
import RoomsPage from './components/roomsPage/roomsPage';
import IdeasPage from './components/ideasPage/ideasPage';
import DesignPage from './components/designPage/designPage';
import Footer from './components/footer/footer';
import LoginPage from './components/loginPage/loginPage';
import RegisterPage from './components/registerPage/registerPage';

import './index.css';
import './reset.css';
import './components/productsPage/productsPage.css';
import './components/header/header.css';
import './components/roomsPage/roomsPage.css';
import './components/ideasPage/ideasPage.css';
import './components/searchPage/searchPage.css';

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <div className='wrapper'>
        {(location.pathname !== '/login') && (
          <div className="Group182">
            <div className='rectangle_2'>
              <div className='Group182Text'>#buy now #вседлядому #швидкотазручно #buy now #вседлядому #швидкотазручно #buy now #вседлядому #швидкотазручно </div>
            </div>
          </div>
        )}
        <Routes>
        <Route path="/register" element={<RegisterPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/" element={<HomePage />} />
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/rooms" element={<RoomsPage />} />
          <Route path="/ideas" element={<IdeasPage />} />
          <Route path="/design" element={<DesignPage />} />
        </Routes>
        <Footer />
      </div>
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);



