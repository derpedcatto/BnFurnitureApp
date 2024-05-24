import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useFormik } from "formik";
import { useNavigate, NavLink } from "react-router-dom";
import { RootState, AppDispatch } from "../../../app/store";
import { loginUser } from "../../../redux/authSlice";
import { validationSchema } from "../validationSchema";
import styles from "./LoginPage.module.scss";
import blackButton from "../../../common/components/buttons/BlackButton.module.scss";
import whiteButton from "../../../common/components/buttons/WhiteButton.module.scss";
import loginImage from "../assets/loginImage.png";
import FormLayout from "../../../common/layouts/authFormLayout";
import FormField from "../../../common/components/form/FormField";

const LoginPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { errors, isLoading, isSuccess } = useSelector((state: RootState) => state.auth);

  const formik = useFormik({
    initialValues: {
      emailOrPhone: "",
      password: "",
    },
    validationSchema,
    onSubmit: async (values) => {
      await dispatch(loginUser(values));
    },
  });

  const { handleChange, handleSubmit, values, errors: formikErrors } = formik;

  React.useEffect(() => {
    if (isSuccess) {
      navigate("/");
    }
  }, [isSuccess, navigate]);

  return (
    <>
      <FormLayout
        isLoading={isLoading}
        handleSubmit={handleSubmit}
        buttonText="ПРОДОВЖИТИ"
        imageSrc={loginImage}
        buttonClass={blackButton["black-button"]}
        imageHeading="ЗАПОВНІТЬ ВХІД ДО ОБЛІКОВОГО ЗАПИСУ"
        imageHeadingColor={styles['image-heading-color']}
        afterSubmitButtonSection={<SignUpSection />}
      >
        <FormField
          label="* Електронна пошта або Номер телефону"
          id="emailOrPhone"
          type="text"
          name="emailOrPhone"
          formik={{ handleChange, values, formikErrors, errors }}
        />
        <FormField
          label="* Пароль"
          id="password"
          type="password"
          name="password"
          formik={{ handleChange, values, formikErrors, errors }}
        />
        <NavLink to={"/auth/forgotpass"} className={styles["forgot-pass-link"]}>
          Забули свій пароль?
        </NavLink>
      </FormLayout>
    </>
  );
};

const SignUpSection: React.FC = () => {
  return(
    <div className={styles['container-signup']}>
      <p>У вас ще немає облікового запису? Створіть його:</p>
      <NavLink to={'/auth/register'} className={`${whiteButton['white-button']} ${styles['signup-button']}`}>СТВОРИТИ АККАУНТ</NavLink>
    </div>
  )
}

export default LoginPage;
