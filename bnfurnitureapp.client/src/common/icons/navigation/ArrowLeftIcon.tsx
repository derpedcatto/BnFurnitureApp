type ArrowLeftIconProps = {
  viewBox?: string;
  fillColor?: string;
  className?: string;
};

const ArrowLeftIcon = ({
  fillColor = "var(--color-icon-black)",
  viewBox = "0 0 40 40",
  className,
}: ArrowLeftIconProps) => {
  return (
    <svg
      viewBox={viewBox}
      fill={fillColor}
      className={className}
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        fillRule="evenodd"
        clipRule="evenodd"
        d="M14 20L22 12L22 28L14 20ZM8.73637e-07 20C7.52784e-07 22.7667 0.525334 25.3667 1.576 27.8C2.62533 30.2333 4.05 32.35 5.85 34.15C7.65 35.95 9.76667 37.3747 12.2 38.424C14.6333 39.4747 17.2333 40 20 40C22.7667 40 25.3667 39.4747 27.8 38.424C30.2333 37.3747 32.35 35.95 34.15 34.15C35.95 32.35 37.3753 30.2333 38.426 27.8C39.4753 25.3667 40 22.7667 40 20C40 17.2333 39.4753 14.6333 38.426 12.2C37.3753 9.76667 35.95 7.65 34.15 5.85C32.35 4.05 30.2333 2.62533 27.8 1.576C25.3667 0.525333 22.7667 -7.53802e-07 20 -8.74818e-07C17.2333 -9.95835e-07 14.6333 0.525332 12.2 1.576C9.76667 2.62533 7.65 4.05 5.85 5.85C4.05 7.65 2.62533 9.76666 1.576 12.2C0.525335 14.6333 9.94491e-07 17.2333 8.73637e-07 20Z"
        fill={fillColor}
      />
    </svg>
  );
};

export default ArrowLeftIcon;
