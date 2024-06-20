import styles from "./CardProductASlider.module.scss";
import { CardProductA, CardProductAProps } from "../../cards";
import useEmblaCarousel from "embla-carousel-react";

export interface CardProductASliderProps {
  products: CardProductAProps[];
  height?: number;
  width?: number;
}

const CardProductASlider: React.FC<CardProductASliderProps> = ({
  products,
  height,
  width,
}) => {
  const finalHeight = height ?? 300;
  const finalWidth = width ?? 210;

  const [emblaRef] = useEmblaCarousel({
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: false,
  });

  return (
    <div className={styles.productCardsCarousel} ref={emblaRef}>
      <div
        className={styles.productCardsContainer}
        style={{ height: finalHeight }}
      >
        {products.map((product, index) => (
          <div
            key={index}
            className={styles.emblaSlide}
            style={{ width: finalWidth }}
          >
            <CardProductA
              name={product.name}
              price={product.price}
              finalPrice={product.finalPrice}
              discount={product.discount}
              categoryString={product.categoryString}
              imageSrc={product.imageSrc}
              topSticker={product.topSticker}
              redirectTo={product.redirectTo}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default CardProductASlider;
