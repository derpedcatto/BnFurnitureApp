import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";
import { RootState, AppDispatch } from "../../../app/store";
import { requestResetPassword } from "../../../redux/authSlice";
import { validationSchema } from "../ValidationSchema";
import styles from "./ForgotPassPage.module.scss";
import FormLayout from "../../../common/layouts/authFormLayout";
import FormField from "../../../common/components/form/FormField";
import blackButton from "../../../common/components/buttons/BlackButton.module.scss";
import forgotPassImage from "../assets/forgotPassImage.png";

const ForgotPassPage = () => {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { isLoading, isSuccess, errors } = useSelector((state: RootState) => state.auth);

  const formik = useFormik({
    initialValues: {
      emailOrPhone: "",
    },
    validationSchema,
    onSubmit: async (values) => {
      await dispatch(requestResetPassword(values));
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
      buttonClass={blackButton["black-button"]}
      imageSrc={forgotPassImage}
      imageHeading="НОВИЙ ПАРОЛЬ"
      imageHeadingColor={styles['image-heading-color']}
    >
      <FormField
        label="* Електронна пошта або Номер телефону"
        id="emailOrPhone"
        type="text"
        name="emailOrPhone"
        formik={{ handleChange, values, formikErrors, errors }}
      />
    </FormLayout>
  );
};

export default ForgotPassPage;
