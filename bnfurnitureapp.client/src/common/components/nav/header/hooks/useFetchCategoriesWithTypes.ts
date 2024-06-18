import { GetAllCategoriesWithProductTypesApiResponse } from "../../../../types/api/responseModels";
import { useFetchApiQueryResponse } from "../../../../hooks/useFetchApiQueryResponse";
import { CategoryWithProductTypes } from "../../../../types/api/responseDataModels";

export const useFetchCategoriesWithTypes = (
  includeImages: boolean = false,
  randomOrder: boolean = false
) => {
  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesWithProductTypesApiResponse>(
      "category/all-with-types",
      {
        includeImages,
        randomOrder,
      }
    );
    
  const categoriesWithTypes: CategoryWithProductTypes[] = response?.data
    ?.list as CategoryWithProductTypes[];

  return { categoriesWithTypes, isLoading, error };
};
