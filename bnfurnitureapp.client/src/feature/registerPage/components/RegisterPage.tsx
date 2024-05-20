import styles from "./RegisterPage.module.scss";
import registerImage from "../assets/registerImage.png";

const RegisterPage = () => {
  const handleSubmit = async (event) => {
    event.preventDefault();
  };

  return (
    <>
      <div className={styles.container}>
        <div className={styles["image-container"]}>
          <img src={registerImage} />
        </div>
        <form onSubmit={handleSubmit} className={styles["form-container"]}>
          <label htmlFor="email">* Електронна пошта</label>
          <input id="email" type="text" name="email" required />

          <label htmlFor="password">* Пароль</label>
          <input id="password" type="password" name="password" required />

          <label htmlFor="repeatPassword">* Повторити Пароль</label>
          <input
            id="repeatPassword"
            type="password"
            name="repeatPassword"
            required
          />

          <label htmlFor="firstName">* Ім'я</label>
          <input id="firstName" type="text" name="firstName" required />

          <label htmlFor="lastName">* Фамілія</label>
          <input id="lastName" type="text" name="lastName" required />

          <label htmlFor="mobileNumber">Номер телефону</label>
          <input id="mobileNumber" type="tel" name="mobileNumber" />

          <label htmlFor="address">Адреса</label>
          <input id="address" type="text" name="address" />

          <fieldset>
            <div className={styles['checkbox-wrapper']}>
              <label>
                <input
                  id="agreeCheckbox"
                  type="checkbox"
                  name="agreeCheckbox"
                  required
                />
                <span className={styles.checkbox} ></span>
              </label>
            </div>
            <label htmlFor="agreeCheckbox" className={styles["gray-text"]}>
              Я ознайомлений з Політикою конфіденційності з обробки та захисту
              персональних даних
            </label>
          </fieldset>

          <button type="submit">Register</button>
        </form>
      </div>
    </>
  );
};

export default RegisterPage;
