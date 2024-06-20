import React from "react";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CardCategoryAProps } from "../../../common/components/cards";
import { GetCategoryTypesApiResponse } from "../../../common/types/api/responseModels";

export const useFetchSliderProductTypes = (categorySlug: string) => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false, // true
      pageNumber: 1,
      pageSize: 10,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetCategoryTypesApiResponse>(
      `category/${categorySlug}/types`,
      fetchOptions
    );
    
  const productTypes: CardCategoryAProps[] | null = React.useMemo(() => {
    // Check if response.data.productTypes is null or an empty array
    if (!response?.data?.productTypes || response.data.productTypes.length === 0) {
      return null;
    } else {
      // Map the response data to productTypes array
      return response.data.productTypes.map((productType) => ({
        categoryName: productType.name,
        imageSrc: productType.cardImageUri,
        redirectTo: `products/${categorySlug}/${productType.slug}`,
      }));
    }
  }, [response]);

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
