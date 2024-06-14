import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CardCategoryAProps } from "../../../common/components/cards";
import { ProductTypesResponse } from "../types/productTypesResponse";

export const useFetchSliderProductTypes = () => {
  const { response, isLoading, error } = useFetchApiQueryResponse<ProductTypesResponse>('producttype/all', {
    includeImages: true,
    randomOrder: false
  });

  const productTypes: CardCategoryAProps[] = response?.data?.productTypes.map(productType => ({
    categoryName: productType.name,
    imageSrc: productType.cardImageUri,
    redirectTo: `/producttype/${productType.slug}`
  }))  || [];

  return { productTypes, isLoading, error };
}

