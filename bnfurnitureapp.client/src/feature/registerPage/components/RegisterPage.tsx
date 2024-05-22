import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { RootState, AppDispatch } from "../../../app/store";
import { registerUser } from "../registerSlice";
import { useFormik } from "formik";
import { validationSchema } from "../validationSchema";
import { useNavigate } from "react-router-dom";
import styles from "./RegisterPage.module.scss";
import registerImage from "../assets/registerImage.png";
import blackButton from "../../../common/components/buttons/BlackButton.module.scss";
import FormLayout from "../../../common/layouts/authFormLayout";
import FormField from "../../../common/components/form/FormField";

const RegisterPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { errors, isLoading, isSuccess } = useSelector(
    (state: RootState) => state.register
  );

  const formik = useFormik({
    initialValues: {
      email: "",
      password: "",
      repeatPassword: "",
      firstName: "",
      lastName: "",
      mobileNumber: "",
      address: "",
      agreeCheckbox: false,
    },
    validationSchema,
    onSubmit: async (values) => {
      const sanitizedValues = {
        ...values,
        mobileNumber:
          values.mobileNumber.trim() === "" ? null : values.mobileNumber,
        address: values.address.trim() === "" ? null : values.address,
      };
      await dispatch(registerUser(sanitizedValues));
    },
  });

  const { handleChange, handleSubmit, values, errors: formikErrors } = formik;

  React.useEffect(() => {
    if (isSuccess) {
      navigate("/auth/login");
    }
  }, [isSuccess, navigate]);

  return (
    <FormLayout
      isLoading={isLoading}
      handleSubmit={handleSubmit}
      buttonText="ПРОДОВЖИТИ"
      imageSrc={registerImage}
      buttonClass={blackButton["black-button"]}
    >
      <FormField
        label="* Електронна пошта"
        id="email"
        type="text"
        name="email"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="* Пароль"
        id="password"
        type="password"
        name="password"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="* Повторити Пароль"
        id="repeatPassword"
        type="password"
        name="repeatPassword"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="* Ім'я"
        id="firstName"
        type="text"
        name="firstName"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="* Фамілія"
        id="lastName"
        type="text"
        name="lastName"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="Номер телефону"
        id="mobileNumber"
        type="tel"
        name="mobileNumber"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <FormField
        label="Адреса"
        id="address"
        type="text"
        name="address"
        formik={{ handleChange, values, formikErrors, errors }}
      />
      <fieldset className={styles.fieldset}>
        <div className={styles["checkbox-wrapper"]}>
          <label>
            <input
              id="agreeCheckbox"
              type="checkbox"
              name="agreeCheckbox"
              onChange={handleChange}
              checked={values.agreeCheckbox}
            />
            <span className={styles.checkbox}></span>
          </label>
        </div>
        <label htmlFor="agreeCheckbox" className={styles["gray-text"]}>
          Я ознайомлений з Політикою конфіденційності з обробки та захисту
          персональних даних
        </label>
      </fieldset>
    </FormLayout>
  );
};

export default RegisterPage;
