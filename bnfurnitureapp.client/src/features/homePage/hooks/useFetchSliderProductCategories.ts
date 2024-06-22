import React from "react";
import { GetAllCategoriesApiResponse } from "../../../common/types/api/responseModels";
import { CardCategoryAProps } from "../../../common/components/cards";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";

export const useFetchSliderProductCategories = () => {
  const fetchOptions = React.useMemo(() => ({
    includeImages: true, // 
    flatList: true,
    randomOrder: false,
    pageNumber: 1,
    pageSize: 10,
  }), []);

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesApiResponse>(
      "category/all",
      fetchOptions
    );

  const categories: CardCategoryAProps[] = React.useMemo(
    () =>
      response?.data?.categories.map((category) => ({
        categoryName: category.name,
        imageSrc: category.cardImageUri,
        redirectTo: `products/${category.slug}`,
      })) || [],
    [response]
  );

  return { categories, isLoading, error };
};

/*
export const useFetchSliderProductCategories = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,
      flatList: true,
      randomOrder: false,
      pageNumber: 1,
      pageSize: 0,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesApiResponse>(
      "category/all",
      fetchOptions
    );

  const categories: CardCategoryAProps[] = React.useMemo(
    () =>
      response?.data?.categories.map((category) => ({
        categoryName: category.name,
        imageSrc: category.cardImageUri,
        redirectTo: `/category/${category.slug}`,
      })) || [],
    [response]
  );

  console.log("useFetchSliderProductCategories", response);

  return { categories, isLoading, error };
};
*/