export interface Category {
  id: string;
  name: string;
  slug: string;
  priority: number | null;
  cardImageUri: string;
  subCategories: Category[] | null;
}

export interface ProductType {
  id: string;
  categoryId: string;
  name: string;
  slug: string;
  priority: number | null;
  categorySlug: string;
  cardImageUri: string;
  thumbnailImageUri: string;
}

export interface CategoryWithProductTypes {
  id: string;
  name: string;
  slug: string;
  priority: number | null;
  cardImageUri: string;
  thumbnailImageUri: string;
  subCategoryList: CategoryWithProductTypes[] | null;
  productTypes: ProductType[] | null;
}