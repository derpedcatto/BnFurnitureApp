export interface CategoriesResponse {
  totalCount: number;
  categories: CategoryResponseDTO[];
}

export interface CategoryResponseDTO {
  id: string;
  name: string;
  slug: string;
  priority: number | null;
  cardImageUri: string;
  subCategories: CategoryResponseDTO[] | null;
}