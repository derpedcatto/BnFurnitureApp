type BnLogoProps = {
  viewBox?: string;
  fillColor?: string;
  className?: string;
};

const BnLogo = ({
  viewBox = "0 0 112 36",
  fillColor = "var(--primary-color)",
  className,
}: BnLogoProps) => {
  return (
    <svg
      viewBox={viewBox}
      fill={fillColor}
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        fillRule="evenodd"
        clipRule="evenodd"
        d="M27 0C12.0883 0 0 12.0883 0 27V36H27V35.9693H40.0878C43.4981 36.1914 46.8788 35.2033 49.6596 33.1717C50.7577 32.2622 51.6307 31.1006 52.2077 29.7812C52.7847 28.4618 53.0494 27.0218 52.9804 25.5782C53.0781 24.0045 52.8103 22.4296 52.199 20.9821C51.613 19.9829 50.8316 18.9838 50.0502 18.3843C49.1008 17.7264 48.0415 17.2523 46.9248 16.9855C47.715 16.8488 48.4695 16.5477 49.1414 16.1009C49.8134 15.6541 50.3887 15.071 50.8316 14.3877C51.613 12.9889 52.199 11.1904 52.199 9.19215C52.199 5.79505 51.0269 3.3971 48.4875 1.99829C46.1434 0.599488 42.6273 0 37.9391 0H27ZM85 0H112V9C112 23.9117 99.9117 36 85 36H76.3685L64.9484 13.2938V36H57V0H65.6336L77.0546 22.7025V0H85ZM38.7205 14.1879C40.8692 14.1879 42.4319 13.9881 43.2133 13.1887C44.19 12.5893 44.5807 11.3903 44.5807 10.1913C44.5807 8.79249 43.9947 7.79335 43.018 7.19386C42.0413 6.59437 40.4785 6.19471 38.3298 6.19471H34.423V14.1879H38.7205ZM34.423 29.9744V20.5824H39.1111C41.4552 20.5824 43.018 20.9821 43.9947 21.7814C44.4441 22.1739 44.8007 22.6655 45.0375 23.2193C45.2744 23.7731 45.3853 24.3747 45.3621 24.9787C45.3621 26.5773 44.9714 27.5765 43.9947 28.5756C43.2133 29.3749 41.4552 29.9744 39.3065 29.9744H34.423Z"
        fill={fillColor}
      />
    </svg>
  );
};

export default BnLogo;
