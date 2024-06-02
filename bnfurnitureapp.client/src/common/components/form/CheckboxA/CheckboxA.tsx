import React from "react";
import { Field } from "formik";
import styles from "./CheckboxA.module.scss";

interface CheckboxAProps {
  id: string;
  name: string;
  checkboxText: React.ReactNode;
}

const CheckboxA: React.FC<CheckboxAProps> = ({ id, name, checkboxText }) => {
  return (
    <div className={styles["checkbox-wrapper"]}>
      <label className={styles["checkbox-container"]}>
        <Field
          id={id}
          type="checkbox"
          name={name}
          className={styles.checkbox}
        />
        <span className={styles.checkbox}></span>
      </label>
      <label htmlFor={id} className={styles["checkbox-text"]}>
        {React.Children.toArray(checkboxText)}
      </label>
    </div>
  );
};

export default CheckboxA;
