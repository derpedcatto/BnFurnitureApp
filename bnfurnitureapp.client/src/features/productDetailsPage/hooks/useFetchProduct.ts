import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { ProductWithCharacteristics } from "../../../common/types/api/responseDataModels";
import { GetProductBySlugApiResponse } from "../../../common/types/api/responseModels";

export const useFetchProduct = (productSlug: string) => {
  const slugParts: string[] = productSlug.split("-");
  const firstPart: string = slugParts.splice(0, 1)[0];

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetProductBySlugApiResponse>(
      `product/${firstPart}`
    );

  const product: ProductWithCharacteristics | null = response?.data?.product ?? null;

  return { product, isLoading, error };
};
