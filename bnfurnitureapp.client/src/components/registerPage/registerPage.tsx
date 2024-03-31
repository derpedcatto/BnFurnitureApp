import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './registerPage.css';

interface FormData {
  firstName: string;
  lastName: string;
  birthDate: string;
  country: string;
  address: string;
  mobileNumber: string;
  email: string;
  password: string;
}

const FormComponent1: React.FC = () => {
  const [formData, setFormData] = useState<FormData>({
    firstName: '',
    lastName: '',
    birthDate: '',
    country: '',
    address: '',
    mobileNumber: '',
    email: '',
    password: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await fetch('url-адреса-контролера', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
      });

      if (response.ok) {
        console.log('Дані успішно відправлені на сервер');
      } else {
        console.error('Сталася помилка при відправленні даних');
      }
    } catch (error) {
      console.error('Помилка при відправленні даних2:', error);
    }
  };

  return (
    <form className="form-container" onSubmit={handleSubmit}>
      <button className='loginVector'>
        <Link to="/">
          <svg width="42" height="42" viewBox="0 0 42 42" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M14.7 21L23.1 29.4L23.1 12.6L14.7 21ZM-9.17939e-07 21C-7.90958e-07 18.095 0.551601 15.365 1.6548 12.81C2.758 10.255 4.2539 8.0325 6.1425 6.1425C8.0325 4.2525 10.255 2.7566 12.81 1.6548C15.365 0.552999 18.095 0.00139896 21 -9.17939e-07C23.905 -7.90958e-07 26.635 0.551599 29.19 1.6548C31.745 2.758 33.9675 4.2539 35.8575 6.1425C37.7475 8.0325 39.2434 10.255 40.3452 12.81C41.447 15.365 41.9986 18.095 42 21C42 23.905 41.4484 26.635 40.3452 29.19C39.242 31.745 37.7461 33.9675 35.8575 35.8575C33.9675 37.7475 31.745 39.2441 29.19 40.3473C26.635 41.4505 23.905 42.0014 21 42C18.095 42 15.365 41.4484 12.81 40.3452C10.255 39.242 8.0325 37.7461 6.1425 35.8575C4.2525 33.9675 2.7559 31.745 1.6527 29.19C0.549502 26.635 -0.00140104 23.905 -9.17939e-07 21Z" fill="black"/>
          </svg>
        </Link>
      </button>

      <div className="nametext">Ім'я</div>
      <input
          className="rec109"
          type="text"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
      />
      
      <div className="nametext1">Фамілія</div>
      <input
          className="rec110"
          type="text"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
      />
      
      <div className="nametext2">Дата народження</div>
      <input
          className="rec111"
          type="text"
          name="birthDate"
          value={formData.birthDate}
          onChange={handleChange}
      />
      
      <div className="nametext4">Країна</div>
      <input
          className="rec113"
          type="text"
          name="country"
          value={formData.country}
          onChange={handleChange}
      />
      
      <div className="nametext5">Адреса</div>
      <input
          className="rec114"
          type="text"
          name="address"
          value={formData.address}
          onChange={handleChange}
      />
      
      <div className="nametext6">Мобільний номер</div>
      <input
          className="rec115"
          type="text"
          name="mobileNumber"
          value={formData.mobileNumber}
          onChange={handleChange}
      />
      
      <div className='text1'>By entering your mobile number and one-time code sign-in option, you agree to receive a one-time verification code via SMS from IKEA. Message and data rates may apply.</div>
      <div className='text2'>More info about Privacy Policy</div>

      <div className="nametext7">Електронна пошта</div>
      <input
          className="rec116"
          type="text"
          name="email"
          value={formData.email}
          onChange={handleChange}
      />
      
      <div className='text3'>Вам потрібно буде пройти верифікацію</div>
      
      <div className="nametext8">Пароль</div>
      <input
          className="rec117"
          type="text"
          name="password"
          value={formData.password}
          onChange={handleChange}
      />
      
      <div className='rect11'></div>
      <div className='textrect11'>Я, ознайомлений з Політикою конфіденційності з обробки та захисту персональних даних</div>

      <button className='butt'>
        <div className='textbut'>продовжити</div>
        </button>
        </form>
        );
      };
      
      export default FormComponent1;
