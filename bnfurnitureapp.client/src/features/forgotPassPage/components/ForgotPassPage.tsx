import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Formik, Form, Field, ErrorMessage } from "formik";
import { useNavigate } from "react-router-dom";
import { RootState, AppDispatch } from "../../../app/store";
import { UserForgotPassProps } from "../../../redux/auth/authTypes";
import { requestResetPassword } from "../../../redux/auth/authThunks";
import { validationSchema } from "../ValidationSchema";
import toast, { Toaster } from "react-hot-toast";
import { AuthFormLayout } from "../../../common/layouts";
import ButtonStyles from '../../../common/styles/Buttons.module.scss';
import FormStyles from "../../../common/styles/Form.module.scss";
import styles from "./ForgotPassPage.module.scss";
import forgotPassImage from "../assets/forgotPassImage.png";

const initialValues: UserForgotPassProps = {
  emailOrPhone: ""
}

const ForgotPassPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { isLoading, isSuccess } = useSelector(
    (state: RootState) => state.user.auth
  );

  React.useEffect(() => {
    if (isSuccess) {
      navigate("/auth/login");
    }
  }, [isSuccess, navigate]);

  return (
    <>
      <Toaster position="bottom-right" />
      <AuthFormLayout
        imageSrc={forgotPassImage}
        imageHeading="НОВИЙ ПАРОЛЬ"
        imageHeadingColor={styles["image-heading-color"]}
      >
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={async (values, { setErrors }) => {
            try {
              const resultAction = await dispatch(requestResetPassword(values));
              if (requestResetPassword.rejected.match(resultAction)) {
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

export default ForgotPassPage;
