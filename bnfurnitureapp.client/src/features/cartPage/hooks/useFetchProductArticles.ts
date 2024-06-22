import { useFetchApiPostQueryResponse } from '../../../common/hooks/useFetchApiQueryResponse';
import { ProductArticle } from '../../../common/types/api/responseDataModels';
import { GetProductArticlesByIdsResponse } from '../../../common/types/api/responseModels';

export const useFetchProductArticles =(articleIdList: string[]) => {
  const { response, isLoading, error } = 
    useFetchApiPostQueryResponse<GetProductArticlesByIdsResponse>(
      `productarticle/articles`,
      { articleList: articleIdList }
    )
  
  const articleList: ProductArticle[] | null = response?.data?.productArticles ?? null;

  return { articleList, isLoading, error }
}

export default useFetchProductArticles;


/*
export const useFetchProductArticles =(articleIdList: string[]) => {
  const { response, isLoading, error } = 
    useFetchApiQueryResponse<GetProductArticleByIdApiResponse>(
      `productarticle/${}`
    )
  
  const articleList: ProductArticle[];

  return { articleList, isLoading, error }
}
*/

