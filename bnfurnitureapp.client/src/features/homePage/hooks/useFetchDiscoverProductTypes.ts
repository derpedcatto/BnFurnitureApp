import React from "react";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { GetAllProductTypesApiResponse } from "../../../common/types/api/responseModels";
import { DiscoverCardSectionBProps } from "../../../common/components/discoverSections/discoverCardSectionB/DiscoverCardSectionB";

interface FetchDiscoverProductTypesParams {
  pageNumber: number;
  pageSize: number;
}

export const useFetchDiscoverProductTypes = ({
  pageNumber,
  pageSize,
}: FetchDiscoverProductTypesParams) => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: true,
      randomOrder: false,
      pageNumber,
      pageSize,
    }),
    [pageNumber, pageSize]
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllProductTypesApiResponse>(
      "producttype/all",
      fetchOptions
    );

  const productTypes: DiscoverCardSectionBProps = React.useMemo(() => {
    const items =
      response?.data?.productTypes.slice(0, 6).map((productType) => ({
        imageSrc: productType.cardImageUri,
        link: `/producttype/${productType.slug}`,
      })) || [];

    // Pad the array to ensure it has exactly 6 elements
    while (items.length < 6) {
      items.push({ imageSrc: "", link: "" });
    }

    return { items } as DiscoverCardSectionBProps;
  }, [response]);

  return { productTypes, isLoading, error };
};
