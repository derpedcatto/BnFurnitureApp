import React from "react";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CardCategoryBProps } from "../../../common/components/cards";
import { GetAllProductTypesApiResponse } from '../../../common/types/api/responseModels';

export const useFetchCatalogProductTypes = () => {
  const fetchOptions = React.useMemo(() => ({
    includeImages: false,  // true
    randomOrder: false,
    pageNumber: 1,
    pageSize: 13,
  }), []);

  const { response, isLoading, error } =
  useFetchApiQueryResponse<GetAllProductTypesApiResponse>(
    "producttype/all",
    fetchOptions
  );

  const catalogProductTypes: CardCategoryBProps[] = React.useMemo(
    () =>
      response?.data?.productTypes.map((productType) => ({
        categoryName: productType.name,
        imageSrc: productType.thumbnailImageUri,
        redirectTo: `/${productType.categorySlug}/${productType.slug}`,
      })) || [],
    [response]
  );

  return { catalogProductTypes, isLoading, error };
};