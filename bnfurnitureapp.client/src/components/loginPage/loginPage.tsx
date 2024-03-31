import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './loginPage.css';


interface FormData {
  emailOrPhone: string;
  password: string;
}

const FormComponent: React.FC = () => {
  const [formData, setFormData] = useState<FormData>({
    emailOrPhone: '',
    password: '',
  });

  const [showPassword, setShowPassword] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Handle form submission here
    console.log(formData);
  };

  return (
    <form className="form-container" onSubmit={handleSubmit}>
      <div className="rectangle-2"></div>
      <div className="frame-16">
        <div className='loginText1'>заповніть вхід до облікового запису</div>
      </div>
      <button className='loginVector'>
        <Link to="/">
          <svg width="42" height="42" viewBox="0 0 42 42" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M14.7 21L23.1 29.4L23.1 12.6L14.7 21ZM-9.17939e-07 21C-7.90958e-07 18.095 0.551601 15.365 1.6548 12.81C2.758 10.255 4.2539 8.0325 6.1425 6.1425C8.0325 4.2525 10.255 2.7566 12.81 1.6548C15.365 0.552999 18.095 0.00139896 21 -9.17939e-07C23.905 -7.90958e-07 26.635 0.551599 29.19 1.6548C31.745 2.758 33.9675 4.2539 35.8575 6.1425C37.7475 8.0325 39.2434 10.255 40.3452 12.81C41.447 15.365 41.9986 18.095 42 21C42 23.905 41.4484 26.635 40.3452 29.19C39.242 31.745 37.7461 33.9675 35.8575 35.8575C33.9675 37.7475 31.745 39.2441 29.19 40.3473C26.635 41.4505 23.905 42.0014 21 42C18.095 42 15.365 41.4484 12.81 40.3452C10.255 39.242 8.0325 37.7461 6.1425 35.8575C4.2525 33.9675 2.7559 31.745 1.6527 29.19C0.549502 26.635 -0.00140104 23.905 -9.17939e-07 21Z" fill="black"/>
          </svg>
        </Link>
      </button> 

      <div className="email-or-phone">Електронна пошта або мобільний номер</div>
      <input
          className="rectangle-109"
          type="text"
          name="emailOrPhone"
          value={formData.emailOrPhone}
          onChange={handleChange}          
      />
      
      <div className="verification-text">By entering your mobile number and one-time code sign-in option, you agree to receive a one-time verification code via SMS from IKEA. Message and data rates may apply.</div>
      <div className="privacy-policy">More info about Privacy Policy</div>
      <div className="password">Пароль</div>
      <div className="password-input">
        <input
          className="form-input rectangle-110"
          type={showPassword ? 'text' : 'password'}
          name="password"
          value={formData.password}
          onChange={handleChange}
        />
        <button
          className="toggle-password"
          type="button"
          onClick={() => setShowPassword(!showPassword)}
        >
          <svg width="22" height="15" viewBox="0 0 22 15" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M11 0C6 0 1.73 3.11 0 7.5C1.73 11.89 6 15 11 15C16 15 20.27 11.89 22 7.5C20.27 3.11 16 0 11 0ZM11 12.5C8.24 12.5 6 10.26 6 7.5C6 4.74 8.24 2.5 11 2.5C13.76 2.5 16 4.74 16 7.5C16 10.26 13.76 12.5 11 12.5ZM11 4.5C9.34 4.5 8 5.84 8 7.5C8 9.16 9.34 10.5 11 10.5C12.66 10.5 14 9.16 14 7.5C14 5.84 12.66 4.5 11 4.5Z" fill="black"/>
          </svg>
        </button>
      </div>

      <div className="forgot-password">Забули свій пароль?</div>
      <button className="component-39 ">
        <div className='dontknow'>продовжити</div>
      </button>
      <div className="create-account"></div>
      <button className="form-button" type="submit">Submit</button>
      <div className="group-96">
        <input className='Rectangle111' type="checkbox" id="saveDataCheckbox" name="saveDataCheckbox" />
        <label className='save-info-text'  htmlFor="saveDataCheckbox">Зберегти дані</label>  
        <button className='svg-con'>
          <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M10 20C4.477 20 0 15.523 0 10C0 4.477 4.477 0 10 0C15.523 0 20 4.477 20 10C20 15.523 15.523 20 10 20ZM9 9V15H11V9H9ZM9 5V7H11V5H9Z" fill="black"/>
          </svg>
      </button>     

    
      </div>
      <div className='dontknow3'>У вас ще немає облікового запису? Створіть його:</div>
      <button className="component-40">
        <div className='dontknow2'>створити аккаунт</div>
      </button>
    </form>
  );
};

export default FormComponent;



