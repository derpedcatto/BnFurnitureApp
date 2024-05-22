import React from "react";
import styles from "./FormField.module.scss";

interface FormFieldProps {
  label: string;
  id: string;
  type: string;
  name: string;
  formik: any;
}

const FormField: React.FC<FormFieldProps> = ({ label, id, type, name, formik }) => {
  const { handleChange, values, formikErrors, errors } = formik;
  return (
    <>
      <label htmlFor={id}>{label}</label>
      <input
        id={id}
        type={type}
        name={name}
        onChange={handleChange}
        value={values[name]}
      />
      {formikErrors[name] && <div className={styles.error}>{formikErrors[name]}</div>}
      {errors?.[name] && errors[name].map((err) => <div key={err} className={styles.error}>{err}</div>)}
    </>
  );
};

export default FormField;