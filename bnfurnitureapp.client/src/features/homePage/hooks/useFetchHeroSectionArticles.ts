import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { ProductArticle } from "../../../common/types/api/responseDataModels";
import { GetProductArticleByCharacteristicsApiResponse } from "../../../common/types/api/responseModels";

export const useFetchHeroSectionArticles = (productSlug: string) => {
  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetProductArticleByCharacteristicsApiResponse>(
      `product/${productSlug}`
    );

  const article: ProductArticle | null = response?.data?.article ?? null;

  return { article, isLoading, error, productSlug  };
};

export default useFetchHeroSectionArticles;
