import {
  Category,
  ProductType,
  CategoryWithProductTypes,
  ProductArticle,
  ProductWithCharacteristics,
  ProductArticleSlug,
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

// ProductController
export interface GetProductBySlugApiResponse {
  product: ProductWithCharacteristics;
}

export interface GetProductArticleByCharacteristicsApiResponse {
  article: ProductArticle;
}

// ProductArticleController
export interface GetProductArticleSlugApiResponse {
  articleSlug: ProductArticleSlug;
}

export interface GetProductArticleByCharacteristicsApiResponse {
  article: ProductArticle;
}

export interface GetProductArticleByIdApiResponse {
  article: ProductArticle;
}

export interface GetProductArticlesByIdsResponse {
  productArticles: ProductArticle[];
}

// [HttpGet("category/{categorySlug}")]
export interface GetProductArticlesByCategoryResponse {
  articles: ProductArticle[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}