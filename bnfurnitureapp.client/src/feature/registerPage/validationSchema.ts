import * as Yup from "yup";

Yup.addMethod(Yup.string, "phoneNumber", function (message) {
  return this.test("phoneNumber", message, function (value) {
    const { path, createError } = this;
    if (!value) return true;
    const isValid = value.length === 12 && value.startsWith("380");
    return isValid || createError({ path, message });
  });
});

export const validationSchema = Yup.object({
  email: Yup.string().email("Електронна пошта не є валідною.").required("Обов'язкове поле."),
  password: Yup.string().min(5, "Пароль має містити хоча б 5 символів.").required("Обов'язкове поле."),
  repeatPassword: Yup.string()
    .oneOf([Yup.ref("password"), null], "Паролі повинні співпадати.")
    .required("Обов'язкове поле."),
  firstName: Yup.string().required("Обов'язкове поле."),
  lastName: Yup.string().required("Обов'язкове поле."),
  mobileNumber: Yup.string().phoneNumber("Невірний формат номера телефону. Приклад валідного номеру = '380XXXXXXXXX'"),
  address: Yup.string(),
  agreeCheckbox: Yup.boolean().oneOf([true], "Потрібна згода."),
});