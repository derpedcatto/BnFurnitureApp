import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { ProductArticle } from '../../../common/types/api/responseDataModels';
import { GetProductArticleByCharacteristicsApiResponse } from "../../../common/types/api/responseModels";

export const useFetchProductArticle = (productSlug: string) => {
  const slugParts: string[] = productSlug.split("-");
  const firstPart: string = slugParts.splice(0, 1)[0];
  const secondPart: string = slugParts.join("-");

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetProductArticleByCharacteristicsApiResponse>(
      `product/${firstPart}-${secondPart}`
    );

  const article: ProductArticle | null = response?.data?.article ?? null;

  return { article, isLoading, error }
};
