type ArrowRightIconProps = {
  viewBox?: string;
  fillColor?: string;
  className?: string;
};

const ArrowRightIcon = ({
  fillColor = "var(--text-black-color)",
  viewBox = "0 0 40 40",
  className,
}: ArrowRightIconProps) => {
  return (
    <svg
      viewBox={viewBox}
      fill={fillColor}
      className={className}
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        d="M26 20L18 12L18 28L26 20ZM40 20C40 22.7667 39.4747 25.3667 38.424 27.8C37.3747 30.2333 35.95 32.35 34.15 34.15C32.35 35.95 30.2333 37.3747 27.8 38.424C25.3667 39.4747 22.7667 40 20 40C17.2333 40 14.6333 39.4747 12.2 38.424C9.76667 37.3747 7.65 35.95 5.85 34.15C4.05 32.35 2.62467 30.2333 1.574 27.8C0.524666 25.3667 -7.53293e-07 22.7667 -8.74228e-07 20C-9.95163e-07 17.2333 0.524665 14.6333 1.574 12.2C2.62467 9.76667 4.05 7.65 5.85 5.85C7.65 4.05 9.76667 2.62533 12.2 1.576C14.6333 0.525333 17.2333 -7.53293e-07 20 -8.74228e-07C22.7667 -9.95163e-07 25.3667 0.525332 27.8 1.576C30.2333 2.62533 32.35 4.05 34.15 5.85C35.95 7.65 37.3747 9.76666 38.424 12.2C39.4747 14.6333 40 17.2333 40 20Z"
        fill={fillColor}
      />
    </svg>
  );
};

export default ArrowRightIcon;