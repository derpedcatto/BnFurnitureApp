export interface ProductTypesResponse {
  productTypes: ProductTypeDTO[];
}

export interface ProductTypeDTO {
  id: string;
  categoryId: string;
  name: string;
  slug: string;
  priority: number | null;
  cardImageUri: string;
}