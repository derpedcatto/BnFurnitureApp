import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { useNavigate, NavLink } from "react-router-dom";
import { RootState, AppDispatch } from "../../../app/store";
import toast, { Toaster } from "react-hot-toast";
import { UserLoginProps, loginUser } from "../../../redux/userSlice";
import { validationSchema } from "../validationSchema";
import { AuthFormLayout } from "../../../common/layouts";
import ButtonStyles from "../../../common/styles/Buttons.module.scss";
import FormStyles from "../../../common/styles/Form.module.scss";
import styles from "./LoginPage.module.scss";
import loginImage from "../assets/loginImage.png";

const initialValues: UserLoginProps = {
  emailOrPhone: "",
  password: "",
};

const LoginPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { isLoading, isSuccess } = useSelector(
    (state: RootState) => state.user.auth
  );

  React.useEffect(() => {
    if (isSuccess) {
      navigate("/");
    }
  }, [isSuccess, navigate]);

  return (
    <>
      <Toaster position="bottom-right" />
      <AuthFormLayout
        imageSrc={loginImage}
        imageHeading="ЗАПОВНІТЬ ВХІД ДО ОБЛІКОВОГО ЗАПИСУ"
        imageHeadingColor={styles["image-heading-color"]}
        afterFormSection={<SignUpSection />}
      >
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={async (values, { setErrors }) => {
            try {
              const resultAction = await dispatch(loginUser(values));
              if (loginUser.rejected.match(resultAction)) {
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
                <label htmlFor="emailOrPhone" className={FormStyles.required}>
                  Електронна пошта або Номер телефону
                </label>
                <Field id="emailOrPhone" type="text" name="emailOrPhone" />
                <ErrorMessage
                  name="emailOrPhone"
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
                <NavLink
                  to={"/auth/forgotpass"}
                  className={styles["forgot-pass-link"]}
                >
                  Забули свій пароль?
                </NavLink>
              </div>
              <button
                type="submit"
                disabled={!(formik.dirty && formik.isValid) || isLoading}
                className={ButtonStyles["button-black"]}
              >
                ПРОДОВЖИТИ
              </button>
            </Form>
          )}
        </Formik>
      </AuthFormLayout>
    </>
  );
};

const SignUpSection: React.FC = () => {
  return (
    <div className={styles["container-signup"]}>
      <p>У вас ще немає облікового запису? Створіть його:</p>
      <NavLink
        to={"/auth/register"}
        className={`${ButtonStyles["button-white"]} ${styles["signup-button"]}`}
      >
        СТВОРИТИ АККАУНТ
      </NavLink>
    </div>
  );
};

export default LoginPage;
