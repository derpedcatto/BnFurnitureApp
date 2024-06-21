import React from "react";
import { GetAllCategoriesApiResponse } from "../../../common/types/api/responseModels";
import { useFetchApiQueryResponse } from "../../../common/hooks/useFetchApiQueryResponse";
import { DiscoverCardSectionBProps } from "../../../common/components/discoverSections/discoverCardSectionB/DiscoverCardSectionB";

export const useFetchDiscoverCategories = () => {
  const fetchOptions = React.useMemo(
    () => ({
      includeImages: false,  // true
      flatList: true,
      randomOrder: false,
      pageNumber: 2,
      pageSize: 6,
    }),
    []
  );

  const { response, isLoading, error } =
    useFetchApiQueryResponse<GetAllCategoriesApiResponse>(
      "category/all",
      fetchOptions
    );

  const categories: DiscoverCardSectionBProps = React.useMemo(() => {
    const items =
      response?.data?.categories.slice(0, 6).map((category) => ({
        imageSrc: category.cardImageUri,
        link: `products/${category.slug}`,
      })) || [];

    // Pad the array to ensure it has exactly 6 elements
    while (items.length < 6) {
      items.push({ imageSrc: "", link: "" });
    }

    return { items } as DiscoverCardSectionBProps;
  }, [response]);

  return { categories, isLoading, error };
};
