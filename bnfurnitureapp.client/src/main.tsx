import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';

import HomePage from './components/homePage/homePage';
import ProductsPage from './components/productsPage/productsPage';
import RoomsPage from './components/roomsPage/roomsPage';
import IdeasPage from './components/ideasPage/ideasPage';
import DesignPage from './components/designPage/designPage';
import Footer from './components/footer/footer';
import LoginPage from './components/loginPage/loginPage';
import RegisterPage from './components/registerPage/registerPage';
import ComponentZero from './components/componentZero/componentZero';
import Header from './components/header/header'; 
import SearchPage from './components/searchPage/searchPage'
import './index.css';
import './reset.css';
import './components/productsPage/productsPage.css';
import './components/header/header.css';
import './components/roomsPage/roomsPage.css';
import './components/ideasPage/ideasPage.css';
import './components/searchPage/searchPage.css';

function App() {
  const location = useLocation();
  const isLoginPage = location.pathname === '/login';
  const isRegisterPage = location.pathname === '/register';
  const isHomePage = location.pathname === "/"
 /* const shouldDisplayHeader = !(isLoginPage || isRegisterPage);*/

  return (
    <div className='wrapper'>
        
      {!isLoginPage && !isRegisterPage && !isHomePage && (
        <> 
          <ComponentZero />        
          <Header />
          <SearchPage />
          
        </>
      )}
      <Routes>
        <Route key="register" path="/register" element={<RegisterPage />} />
        <Route key="login" path="/login" element={<LoginPage />} />
        <Route key="home" path="/" element={<HomePage />} />
        <Route key="products" path="/products" element={<ProductsPage />} />
        <Route key="rooms" path="/rooms" element={<RoomsPage />} />
        <Route key="ideas" path="/ideas" element={<IdeasPage />} />
        <Route key="design" path="/design" element={<DesignPage />} />
      </Routes>
      {!isLoginPage && !isRegisterPage && <Footer />}
    </div>
  );
}

ReactDOM.render(
  <React.StrictMode>
    <Router>
      <App />
    </Router>
  </React.StrictMode>,
  document.getElementById('root')
);




 



