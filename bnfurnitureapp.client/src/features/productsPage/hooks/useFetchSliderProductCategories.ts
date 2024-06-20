import React from "react";
import { CardCategoryAProps } from "../../../common/components/cards";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { GetAllSubCategoriesApiResponse } from "../../../common/types/api/responseModels";

export const useFetchSliderProductCategories = (categorySlug: string) => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false, // true
      flatList: true,
      randomOrder: false,
      pageNumber: 1,
      pageSize: 10,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllSubCategoriesApiResponse>(
      `category/${categorySlug}`,
      fetchOptions
    );

  const categories: CardCategoryAProps[] | null = React.useMemo(() => {
    // Check if response.data.subCategories is null or an empty array
    if (!response?.data?.subCategories || response.data.subCategories.length <= 0) {
      return null;
    }
    else {
      // Map the response data to categories array
      return response.data.subCategories.map((category) => ({
        categoryName: category.name,
        imageSrc: category.cardImageUri,
        redirectTo: `/products/${category.slug}`,
      }));
    }


  }, [response]);

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
