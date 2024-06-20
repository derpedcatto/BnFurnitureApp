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
  subCategories: CategoryWithProductTypes[] | null;
  productTypes: ProductType[] | null;
}

/* --- */

export interface ProductWithCharacteristics {
  id: string;
  productTypeId: string;
  authorId: string;
  name: string;
  slug: string;
  summary: string | null;
  description: string | null;
  productDetails: string | null;
  priority: number | null;
  active: boolean;
  createdAt: string;
  updatedAt: string | null;
  characteristics: CharacteristicWithValues[];
}

export interface CharacteristicWithValues {
  id: string;
  name: string;
  slug: string;
  priority: number | null;
  values: CharacteristicValue[];
}

export interface CharacteristicValue {
  id: string;
  characteristicId: string;
  value: string;
  slug: string;
  priority: number | null;
}