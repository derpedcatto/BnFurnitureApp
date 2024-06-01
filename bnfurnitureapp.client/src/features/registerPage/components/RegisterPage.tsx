import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Formik, Form, Field, ErrorMessage } from "formik";
import toast, { Toaster } from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { RootState, AppDispatch } from "../../../app/store";
import { registerUser } from "../../../redux/userSlice";
import { validationSchema } from "../validationSchema";
import { AuthFormLayout } from "../../../common/layouts";
import { UserRegisterProps } from "../../../redux/userSlice";
import { CheckboxA } from "../../../common/components/form";
import FormStyles from "../../../common/styles/Form.module.scss";
import ButtonStyles from "../../../common/styles/Buttons.module.scss";
import registerImage from "../assets/registerImage.png";
// import styles from "./RegisterPage.module.scss";

const initialValues: UserRegisterProps = {
  email: "",
  password: "",
  repeatPassword: "",
  firstName: "",
  lastName: "",
  mobileNumber: "",
  address: "",
  agreeCheckbox: false,
};

const RegisterPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { isLoading, isSuccess } = useSelector(
    (state: RootState) => state.user.register
  );

  React.useEffect(() => {
    if (isSuccess) {
      navigate("/auth/login");
    }
  }, [isSuccess, navigate]);

  return (
    <>
      <Toaster position="bottom-right" />
      <AuthFormLayout imageSrc={registerImage}>
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={async (values, { setErrors }) => {
            try {
              const resultAction = await dispatch(registerUser(values));
              if (registerUser.rejected.match(resultAction)) {
                if (resultAction.payload?.errors) {
                  toast.error(resultAction.payload?.message);
                  setErrors(resultAction.payload.errors);
                }
              }
            } catch (error: unknown) {
              toast.error(String(error));
            }
          }}
        >
          {(formik) => (
            <Form className={FormStyles["form-container"]}>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="email" className={FormStyles.required}>
                  Електронна пошта
                </label>
                <Field id="email" type="text" name="email" />
                <ErrorMessage
                  name="email"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="password" className={FormStyles.required}>
                  Пароль
                </label>
                <Field id="password" type="password" name="password" />
                <ErrorMessage
                  name="password"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="repeatPassword" className={FormStyles.required}>
                  Повторити пароль
                </label>
                <Field
                  id="repeatPassword"
                  type="password"
                  name="repeatPassword"
                />
                <ErrorMessage
                  name="repeatPassword"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="firstName" className={FormStyles.required}>
                  Ім'я
                </label>
                <Field id="firstName" type="text" name="firstName" />
                <ErrorMessage
                  name="firstName"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="lastName" className={FormStyles.required}>
                  Фамілія
                </label>
                <Field id="lastName" type="text" name="lastName" />
                <ErrorMessage
                  name="lastName"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="mobileNumber">Номер телефону</label>
                <Field id="mobileNumber" type="tel" name="mobileNumber" />
                <ErrorMessage
                  name="mobileNumber"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <div className={FormStyles["form-field-container"]}>
                <label htmlFor="address">Адреса</label>
                <Field id="address" type="text" name="address" />
                <ErrorMessage
                  name="address"
                  component="div"
                  className={FormStyles.error}
                />
              </div>
              <CheckboxA
                id="agreeCheckbox"
                name="agreeCheckbox"
                checkboxText={
                  <>
                    Я ознайомлений з{" "}
                    <a href="/privacy-policy">Політикою конфіденційності</a> з
                    обробки та захисту персональних даних
                  </>
                }
              />

              <button
                type="submit"
                disabled={!(formik.dirty && formik.isValid) || isLoading}
                className={ButtonStyles["button-black"]}
              >
                РЕЄСТРУВАТИ
              </button>
            </Form>
          )}
        </Formik>
      </AuthFormLayout>
    </>
  );
};

export default RegisterPage;
