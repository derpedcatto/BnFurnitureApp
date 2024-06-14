import { CategoriesResponse } from '../types/categoriesResponse';
import { CardCategoryAProps } from '../../../common/components/cards';
import { useFetchApiQueryResponse } from '../../../common/hooks/useFetchApiQueryResponse';

export const useFetchSliderProductCategories = () => {
  const { response, isLoading, error } = useFetchApiQueryResponse<CategoriesResponse>('category/all', {
    includeImages: true,
    flatList: true,
    randomOrder: false
  });

  const categories: CardCategoryAProps[] = response?.data?.categories.map(category => ({
    categoryName: category.name,
    imageSrc: category.cardImageUri,
    redirectTo: `/category/${category.slug}`
  }))  || [];

  return { categories, isLoading, error };
};

/*
interface Category {
  categoryName: string;
  imageSrc: string;
  redirectTo: string;
}
  const [categories, setCategories] = useState<Category[]>([]);
*/