import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { ProductArticle } from "../../../common/types/api/responseDataModels";
import { GetProductArticlesByCategoryResponse } from "../../../common/types/api/responseModels";
import React from "react";

export const useFetchProductArticles = (
  pageUrl: string,
  pageNumber: number,
  pageSize: number
) => {
  const fetchOptions = React.useMemo(
    () => ({
      pageNumber: pageNumber,
      pageSize: pageSize,
    }),
    [pageNumber, pageSize]
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetProductArticlesByCategoryResponse>(
      `productarticle/category/${pageUrl}`,
      fetchOptions
    );

  const articleList: ProductArticle[] | null = response?.data?.articles ?? null;
  const totalCount: number = response?.data?.totalCount ?? 0;

  return { articleList, totalCount, isLoading, error };
};

export default useFetchProductArticles;
