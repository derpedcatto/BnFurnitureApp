import styles from "./CartPage.module.scss";
import {
  getAllItemsFromCart,
  CartItem,
  removeItemFromCart,
} from "../../common/utils/CartUtils";
import { useState, useEffect } from "react";
import { LoadingSpinner } from "../../common/components/ui";
import useFetchProductArticles from "./hooks/useFetchProductArticles";
import { CardProductA } from "../../common/components/cards";
import { useNavigate } from "react-router-dom";

const CartPage: React.FC = () => {
  const navigate = useNavigate();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [articleIds, setArticleIds] = useState<string[]>([]);
  const { articleList, isLoading, error } = useFetchProductArticles(articleIds);

  const handleBuy = () => {
    navigate("/buy");
  };

  useEffect(() => {
    updateCartItems();
  }, []);

  const updateCartItems = () => {
    const items = getAllItemsFromCart();
    setCartItems(items);
    setArticleIds(items.map((item) => item.articleId));
  };

  const handleRemoveItem = (articleId: string) => {
    removeItemFromCart(articleId);
    updateCartItems();
  };

  useEffect(() => {
    const items = getAllItemsFromCart();
    setCartItems(items);
    setArticleIds(items.map((item) => item.articleId));
  }, []);

  // Merge cart items with fetched articles, keeping the slug from cartItems
  const mergedCartData = cartItems.map((cartItem) => {
    const article = articleList?.find(
      (article) => article.article === cartItem.articleId
    );
    return {
      ...cartItem,
      article: article || null,
    };
  });

  return (
    <div className={styles.cartPageContainer}>
      <h1>КОШИК</h1>
      {isLoading ? (
        <div className={styles.loadingSpinnerPageContainer}>
        <div className={styles.loadingSpinnerContainer}>
          <LoadingSpinner />
        </div>
        </div>
      ) : error ? (
        <p>Error fetching product articles: {error.message}</p>
      ) : cartItems.length > 0 ? (
        <div className={styles.cardsContainer}>
          {mergedCartData.map((item, index) => {
            if (!item.article) return null;
            return (
              <div key={index} className={styles.cardContainer}>
                <button
                  onClick={() => handleRemoveItem(item.articleId)}
                  className={styles.removeButton}
                  aria-label="Remove item"
                >
                  &times;
                </button>
                <CardProductA
                  name={item.article.name}
                  categoryString={item.article.productTypeName}
                  price={`${item.article.price.toFixed(2)}`}
                  finalPrice={`${item.article.finalPrice}`}
                  discount={item.article.discount}
                  imageSrc={item.article.thumbnailImageUri}
                  topSticker={false}
                  redirectTo={`/product-details/${item.slug}`}
                />
              </div>
            );
          })}
        </div>
      ) : (
        <div>Ваш кошик пустий.</div>
      )}
      {cartItems.length > 0 ? (
        <button onClick={handleBuy} className={styles.buyButton}>
          ОФОРМИТИ ЗАМОВЛЕННЯ
        </button>
      ) : (
        <></>
      )}
    </div>
  );
};

export default CartPage;

/*
const CartPage: React.FC = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  
  useEffect(() => {
    const items = getAllItemsFromCart();
    setCartItems(items);
  }, []);

  return (
    <div className={styles.cartPageContainer}>
      <h1>КОШИК</h1>
      {cartItems.length > 0 ? (
        <ul>
          {cartItems.map((item, index) => (
            <li key={index}>
              <NavLink to={`/product-details/${item.slug}`}>{item.articleId} - {item.slug}</NavLink>
            </li>
          ))}
        </ul>
      ) : (
        <p>Your cart is empty</p>
      )}
    </div>
  )
}

export default CartPage;
*/
