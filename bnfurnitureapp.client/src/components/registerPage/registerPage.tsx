import React, { useState, useEffect } from 'react';

import { Link  } from 'react-router-dom'; 
import './registerPage.css';

interface FormData {
  firstName: string;
  lastName: string;
  address: string;
  mobileNumber: string;
  email: string;
  password: string;
}

const FormComponent1: React.FC = () => {

  const [formData, setFormData] = useState<FormData>({
    firstName: '',
    lastName: '',
    address: '',
    mobileNumber: '',
    email: '',
    password: '',
  });
  const [errors, setErrors] = useState<Partial<FormData>>({});
  const [submitted, setSubmitted] = useState<boolean>(false); 
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    setErrors({ ...errors, [name]: '' }); // Clear the error for this field
  };
  const [isChecked, setIsChecked] = useState<boolean>(false);
  const [errorMessage, setErrorMessage] = useState<string>('');
  
  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setIsChecked(e.target.checked);
    setErrorMessage('');
  };
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    const newErrors: Partial<FormData> = {};

    // Валідація складності паролю (мінімум 8 символів, включаючи цифри та літери)
    if (formData.password.trim().length < 8 || !/\d/.test(formData.password) || !/[a-zA-Z]/.test(formData.password)) {
      newErrors.password = 'Пароль повинен містити принаймні 8 символів, включаючи цифри та літери';
    }

    // Валідація номера телефону для України (10 цифр, може бути з або без "+", може мати пробіли)
    if (!/^(\+?38)?(\s?\d{3}){3}\s?\d{2}\s?\d{2}$/.test(formData.mobileNumber.trim())) {
      newErrors.mobileNumber = 'Будь ласка, введіть дійсний номер телефону для України';
    }

    // Валідація електронної пошти
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email.trim())) {
      newErrors.email = 'Будь ласка, введіть дійсну адресу електронної пошти';
    }

    // Валідація імені (тільки літери, може містити апострофи)|| !/^[a-zA-Zа-яА-ЯІіЇїЄєҐґ'ґ\s]+$/.test(formData.lastName.trim())) {
      if (!/^[a-zA-Zа-яА-ЯІіЇїЄєҐґ'ґ\s]+$/.test(formData.firstName.trim())) {
        newErrors.firstName = 'Ім\'я можуть містити тільки літери';
      }
  
    // Валідація  прізвища (тільки літери, може містити апострофи)
    if (!/^[a-zA-Zа-яА-ЯІіЇїЄєҐґ'ґ\s]+$/.test(formData.lastName.trim())) {
      newErrors.lastName = 'Фамілія можуть містити тільки літери';
    }
    // Валідація Адреси (тільки літери, може містити апострофи) ЗМІНИТИ БО МОЖЕ БУТИ ПУСТИМ
    if (!/^[a-zA-Zа-яА-ЯІіЇїЄєҐґ'ґ\s]+$/.test(formData.address.trim())) {
      
      newErrors.address = 'Адреса може містити тільки літери';
    }
    if (!isChecked) {
      setErrorMessage('Будь ласка, ознайомтеся з Політикою конфіденційності та підтвердіть її');
      return;
    }
    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    try {
      const response = await fetch('url-адреса-контролера', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
      });

      const data = await response.json(); //отримуємо відповідь від сервера у форматі JSON і розбирає її в об'єкт JavaScript

    if (response.ok) {
      setSubmitted(true);
      console.log('Дані успішно відправлені на сервер');
    } else {
      setErrorMessage(data.message); // Показати повідомлення про помилку з сервера
      console.error('Сталася помилка при відправленні даних');
    }
  } catch (error) {
    console.error('Помилка при відправленні даних:', error);
    setErrorMessage('Сталася помилка при відправленні даних');
  }
};

  useEffect(() => {
    if (submitted) {
      // Перенаправлення на головну сторінку після успішного відправлення форми
      window.location.replace("/");
    }
  }, [submitted]);

  return (
    <div className='register'>
      <div className='registerLeft'>
        <button className='loginVector'>
          <Link to="/">
            <svg width="42" height="42" viewBox="0 0 42 42" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M14.7 21L23.1 29.4L23.1 12.6L14.7 21ZM-9.17939e-07 21C-7.90958e-07 18.095 0.551601 15.365 1.6548 12.81C2.758 10.255 4.2539 8.0325 6.1425 6.1425C8.0325 4.2525 10.255 2.7566 12.81 1.6548C15.365 0.552999 18.095 0.00139896 21 -9.17939e-07C23.905 -7.90958e-07 26.635 0.551599 29.19 1.6548C31.745 2.758 33.9675 4.2539 35.8575 6.1425C37.7475 8.0325 39.2434 10.255 40.3452 12.81C41.447 15.365 41.9986 18.095 42 21C42 23.905 41.4484 26.635 40.3452 29.19C39.242 31.745 37.7461 33.9675 35.8575 35.8575C33.9675 37.7475 31.745 39.2441 29.19 40.3473C26.635 41.4505 23.905 42.0014 21 42C18.095 42 15.365 41.4484 12.81 40.3452C10.255 39.242 8.0325 37.7461 6.1425 35.8575C4.2525 33.9675 2.7559 31.745 1.6527 29.19C0.549502 26.635 -0.00140104 23.905 -9.17939e-07 21Z" fill="black"/>
            </svg>
          </Link>
        </button>
      </div>
      
      <form className="formRegister" onSubmit={handleSubmit}>
      <div className="registerText1">Ім'я</div>
      <input
        className="registerInput1"
        type="text"
        name="firstName"
        value={formData.firstName}
        onChange={handleChange}
      />
      {errors.firstName && <div className="error">{errors.firstName}</div>}

      <div className="registerText2">Фамілія</div>
      <input
        className="registerInput2"
        type="text"
        name="lastName"
        value={formData.lastName}
        onChange={handleChange}
      />
      {errors.lastName && <div className="error">{errors.lastName}</div>}

      <div className="registerText2">Адреса</div>
      <input
        className="registerInput3"
        type="text"
        name="address"
        value={formData.address}
        onChange={handleChange}
      />
      {errors.address && <div className="error">{errors.address}</div>}
      <div className="registerText4">Мобільний номер</div>
      <input
        className="registerInput4"
        type="text"
        name="mobileNumber"
        value={formData.mobileNumber}
        onChange={handleChange}
      />
      <div className="registerText5">Електронна пошта</div>
      <input
        className="registerInput5"
        type="text"
        name="email"
        value={formData.email}
        onChange={handleChange}
      />
      {errors.email && <div className="error">{errors.email}</div>}

      <div className="registerText6">Пароль</div>
      <input
        className="registerInput6"
        type="password"
        name="password"
        value={formData.password}
        onChange={handleChange}
      />
      {errors.password && <div className="error">{errors.password}</div>}
      <Link to="/">
      <div className="registerTextPrivacy">     
      <u>More info about Privacy Policy</u>
      </div>
     </Link>
      <div className="registerCheck">
      <input
        className='registerCheckbox'
        type="checkbox"
        id="saveDataCheckbox"
        name="saveDataCheckbox"
        checked={isChecked}
        onChange={handleCheckboxChange}
      />
      
      <div className="registerCheckText">     
        Я, ознайомлений з Політикою конфіденційності з обробки та захисту персональних даних
      </div>
    
  </div>
  {errorMessage && <div className="error">{errorMessage}</div>}
    <button className="buttonRegister">
      <div className="buttonRegisterText">продовжити</div>
    </button>
  </form>
  </div>
  );
};

export default FormComponent1;      
       



