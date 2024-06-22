import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { GetProductArticleSlugApiResponse } from "../../../common/types/api/responseModels";

export const useFetchProductArticleSlug = (articleId: string) => {
  const { response, isLoading } =
    useFetchApiQueryResponse<GetProductArticleSlugApiResponse>(
      `productarticle/slug/${articleId}`
    );

  const articleSlug: string | null = response?.data?.articleSlug.slug ?? null;

  return { articleSlug, isLoading };
};
