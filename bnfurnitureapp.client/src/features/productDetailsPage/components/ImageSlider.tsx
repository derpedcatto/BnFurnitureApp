import useEmblaCarousel from "embla-carousel-react";
import { useEffect, useState } from "react";
import styles from "./ImageSlider.module.scss";

export interface ImageSliderProps {
  imageUris: string[];
}

export const ImageSlider: React.FC<ImageSliderProps> = ({ imageUris }) => {
  const [selectedIndex, setSelectedIndex] = useState<number>(0);
  const [breakpoint, setBreakpoint] = useState<string>("768px");
  const [emblaRef] = useEmblaCarousel({
    axis: "x",
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: false,
    breakpoints: {
      [`(min-width: ${breakpoint})`]: {
        axis: "y",
      },
      [`(max-width: ${breakpoint})`]: {
        axis: "x",
      },
    },
  });

  // Get CSS breakpoint
  useEffect(() => {
    const rootStyle = getComputedStyle(document.documentElement);
    const breakpointValue = rootStyle.getPropertyValue("--bp-w-md").trim();
    setBreakpoint(breakpointValue);
  }, []);

  const handleImageClick = (index: number) => {
    setSelectedIndex(index);
  };

  return (
    <div className={styles.imageSlider}>
      <div className={styles.thumbnailContainer} ref={emblaRef}>
        <div className={styles.emblaContainer}>
          {imageUris.map((uri, index) => (
            <div className={`${styles.emblaImageContainer}`}>
              <img
                src={uri}
                alt={`Thumbnail ${index}`}
                key={index}
                onClick={() => handleImageClick(index)}
                className={`${styles.thumbnail} ${
                  selectedIndex === index ? styles.selected : ""
                }`}
              />
            </div>
          ))}
        </div>
      </div>
      <div className={styles.mainImageContainer}>
        <img
          src={imageUris[selectedIndex]}
          alt={`Preview ${selectedIndex}`}
          className={styles.mainImage}
        />
      </div>
    </div>
  );
};

/*
  const [emblaRef, emblaApi] = useEmblaCarousel({
    axis: "y",
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: false,
    loop: false,
  });

  useEffect(() => {
    function updateBreakpoint() {
      if (window.innerWidth >= 768) {
        emblaApi?.reInit({ axis: "x" });
      } else {
        emblaApi?.reInit({ axis: "y" });
      }
    }

    window.addEventListener("resize", updateBreakpoint);
    updateBreakpoint(); // Initial call

    return () => window.removeEventListener("resize", updateBreakpoint);
  }, [emblaApi]);
*/

/*
    const [breakpoint, setBreakpoint] = useState<string>('768px');
  const [emblaRef] = useEmblaCarousel({
    axis: 'y',
    containScroll: "trimSnaps",
    align: "start",
    skipSnaps: true,
    loop: false,
    breakpoints: {
      [`(min-width: ${breakpoint})`]: {
        axis: 'x',
      },
    },
  });

  // Get CSS breakpoint
  useEffect(() => {
    const rootStyle = getComputedStyle(document.documentElement);
    const breakpointValue = rootStyle.getPropertyValue('--bp-w-md').trim();
    setBreakpoint(breakpointValue);
  }, []);
  */
