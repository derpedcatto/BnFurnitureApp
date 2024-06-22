import { GetAllCategoriesWithProductTypesApiResponse } from "../../../../types/api/responseModels";
import { useFetchApiQueryResponse } from "../../../../hooks/useFetchApiQueryResponse";
import { CategoryWithProductTypes } from "../../../../types/api/responseDataModels";
import React from "react";

export const useFetchCategoriesWithTypes = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,
      randomOrder: false,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesWithProductTypesApiResponse>(
      "category/all-with-types",
      fetchOptions
    );
    
  const categoriesWithTypes: CategoryWithProductTypes[] = response?.data
    ?.list as CategoryWithProductTypes[];

  return { categoriesWithTypes, isLoading, error };
};