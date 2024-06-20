import {
  Category,
  ProductType,
  CategoryWithProductTypes,
} from "./responseDataModels";

// CategoryController
export interface GetAllCategoriesApiResponse {
  totalCount: number;
  categories: Category[];
}

export interface GetAllCategoriesWithProductTypesApiResponse {
  list: CategoryWithProductTypes[];
}

export interface GetAllSubCategoriesApiResponse {
  subCategories: Category[] | null;
}

export interface GetCategoryTypesApiResponse {
  productTypes: ProductType[] | null;
}

// ProductTypeController
export interface GetAllProductTypesApiResponse {
  productTypes: ProductType[];
}
