import * as Yup from "yup";

export const validationSchema = Yup.object({
  emailOrPhone: Yup.string().required("Обов'язкове поле."),
});
