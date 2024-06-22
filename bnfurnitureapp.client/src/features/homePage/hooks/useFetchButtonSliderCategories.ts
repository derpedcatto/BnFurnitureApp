import React from "react";
import { GetAllCategoriesApiResponse } from "../../../common/types/api/responseModels";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CategoryNamesButtonSliderProps } from "../../../common/components/sliders/CategoryNamesButtonSlider/CategoryNamesButtonSlider";

export const useFetchButtonSliderCategories = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,
      flatList: true,
      randomOrder: false,
      pageNumber: 2,
      pageSize: 10,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesApiResponse>(
      "category/all",
      fetchOptions
    );

  const categories: CategoryNamesButtonSliderProps['categories'] = React.useMemo(
    () =>
      response?.data?.categories.map((category) => ({
        categoryName: category.name,
        redirectTo: `products/${category.slug}`,
      })) || [],
    [response]
  );

  return { categories, isLoading, error };
};
