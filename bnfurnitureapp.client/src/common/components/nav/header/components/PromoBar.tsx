import { useEffect, useRef } from "react";
import styles from "./PromoBar.module.scss";

const PromoBar = () => {
  const scrollerRef = useRef<HTMLDivElement>(null);

  const addAnimation = () => {
    const scroller = scrollerRef.current;
    if (!scroller) return;

    const scrollerInner = scroller.querySelector<HTMLUListElement>(
      `.${styles["scroller-inner"]}`
    );
    if (!scrollerInner) return;

    const scrollerContent = Array.from(scrollerInner.children).slice(0, 3); // Original items only

    for (let i = 0; i < 3; i++) {
      scrollerContent.forEach((item) => {
        const duplicatedItem = item.cloneNode(true) as HTMLElement;
        duplicatedItem.setAttribute("aria-hidden", "true");
        scrollerInner.appendChild(duplicatedItem);
      });
    }

    // Restart animation
    scrollerInner.style.animation = "none";
    requestAnimationFrame(() => {
      scrollerInner.style.animation = "";
    });
  };

  useEffect(() => {
    addAnimation();
  }, []);

  const promoContent = (
    <>
      <li>#buynow</li>
      <li>#вседлядому</li>
      <li>#швидкотазручно</li>
    </>
  );

  return (
    <>
      <div className={styles.container}>
        <div className={styles.scroller} ref={scrollerRef}>
          <ul className={`${styles["tag-list"]} ${styles["scroller-inner"]}`}>
            {promoContent}
          </ul>
        </div>
      </div>
    </>
  );
};

export default PromoBar;
