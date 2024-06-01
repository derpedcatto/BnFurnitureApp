import * as Yup from "yup";

export const validationSchema = Yup.object({
  emailOrPhone: Yup.string().required("Обов'язкове поле."),
  password: Yup.string().min(5, "Пароль має містити хоча б 5 символів.").required("Обов'язкове поле."),
});
