export interface CartItem {
  articleId: string;
  slug: string;
}

export const addItemToCart = (articleId: string, slug: string) => {
  const cart: CartItem[] = JSON.parse(localStorage.getItem('cart') || '[]');
  if (!cart.find(item => item.articleId === articleId)) {
    cart.push({ articleId, slug });
    localStorage.setItem('cart', JSON.stringify(cart));
  }
};

export const removeItemFromCart = (articleId: string) => {
  let cart: CartItem[] = JSON.parse(localStorage.getItem('cart') || '[]');
  cart = cart.filter(item => item.articleId !== articleId);
  localStorage.setItem('cart', JSON.stringify(cart));
};

export const isItemInCart = (articleId: string): boolean => {
  const cart: CartItem[] = JSON.parse(localStorage.getItem('cart') || '[]');
  return cart.some(item => item.articleId === articleId);
};

export const toggleItemInCart = (articleId: string, slug: string) => {
  if (isItemInCart(articleId)) {
    removeItemFromCart(articleId);
  } else {
    addItemToCart(articleId, slug);
  }
};

export const getAllItemsFromCart = (): CartItem[] => {
  return JSON.parse(localStorage.getItem('cart') || '[]');
};