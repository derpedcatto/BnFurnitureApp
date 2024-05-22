import * as Yup from 'Yup';

declare module 'yup' {
  interface StringSchema extends Yup.StringSchema {
    phoneNumber(message: string): StringSchema;
  }
}