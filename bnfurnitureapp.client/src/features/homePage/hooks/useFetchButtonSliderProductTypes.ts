import React from "react";
import { GetAllProductTypesApiResponse } from "../../../common/types/api/responseModels";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { CategoryNamesButtonSliderProps } from "../../../common/components/sliders/CategoryNamesButtonSlider/CategoryNamesButtonSlider";

export const useFetchButtonSliderProductTypes = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,
      randomOrder: false,
      pageNumber: 2,
      pageSize: 10,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllProductTypesApiResponse>(
      "producttype/all",
      fetchOptions
    );

    const productTypes: CategoryNamesButtonSliderProps['categories'] = React.useMemo(
      () =>
        response?.data?.productTypes.map((productTypes) => ({
          categoryName: productTypes.name,
          redirectTo: `/producttype/${productTypes.slug}`,
        })) || [],
      [response]
    );
  
    return { productTypes, isLoading, error };
};
