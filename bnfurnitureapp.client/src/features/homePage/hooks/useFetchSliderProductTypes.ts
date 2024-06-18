import React from "react";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CardCategoryAProps } from "../../../common/components/cards";
import { GetAllProductTypesApiResponse } from "../../../common/types/api/responseModels";

export const useFetchSliderProductTypes = () => {
  const fetchOptions = React.useMemo(() => ({
    includeImages: true,
    randomOrder: false,
    pageNumber: 1,
    pageSize: 10,
  }), []);

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllProductTypesApiResponse>(
      "producttype/all",
      fetchOptions
    );

  const productTypes: CardCategoryAProps[] = React.useMemo(
    () =>
      response?.data?.productTypes.map((productType) => ({
        categoryName: productType.name,
        imageSrc: productType.cardImageUri,
        redirectTo: `/${productType.categorySlug}/${productType.slug}`,
      })) || [],
    [response]
  );

  return { productTypes, isLoading, error };
};

/*
export const useFetchSliderProductTypes = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,
      randomOrder: false,
      pageNumber: 1,
      pageSize: 0,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllProductTypesApiResponse>(
      "producttype/all",
      fetchOptions
    );

  const productTypes: CardCategoryAProps[] = React.useMemo(
    () =>
      response?.data?.productTypes.map((productType) => ({
        categoryName: productType.name,
        imageSrc: productType.cardImageUri,
        redirectTo: `/producttype/${productType.slug}`,
      })) || [],
    [response]
  );

  console.log("useFetchSliderProductTypes", response);

  return { productTypes, isLoading, error };
};
*/