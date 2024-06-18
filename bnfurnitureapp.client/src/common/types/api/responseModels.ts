import { Category, ProductType, CategoryWithProductTypes } from "./responseDataModels";

export interface GetAllCategoriesApiResponse {
  totalCount: number;
  categories: Category[];
}

export interface GetAllCategoriesWithProductTypesApiResponse {
  list: CategoryWithProductTypes[];
}

export interface GetAllProductTypesApiResponse {
  productTypes: ProductType[];
}