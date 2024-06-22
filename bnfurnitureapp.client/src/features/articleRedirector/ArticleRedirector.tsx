import { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useFetchProductArticleSlug } from './hooks/useFetchProductArticleSlug';

function useProductArticleSlug(articleId: string) {
  const {articleSlug, isLoading} 
    = useFetchProductArticleSlug(articleId);

    return {articleSlug, isLoading};
}

const ArticleRedirector: React.FC = () => {
  const { article } = useParams<{ article: string }>();
  const { articleSlug, isLoading } = useProductArticleSlug(article ?? '');
  const navigate = useNavigate();

  console.log('articleSlug', articleSlug);
  useEffect(() => {
    if (!isLoading && articleSlug) {
      navigate(`/product-details/${articleSlug}`, { replace: true });
    }
  }, [isLoading, articleSlug, navigate]);

  return null;
};

export default ArticleRedirector;