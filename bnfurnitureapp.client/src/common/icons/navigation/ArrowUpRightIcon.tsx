type ArrowUpRightIconProps = {
  viewBox?: string;
  fillColor?: string;
  fillColorBackground?: string;
  className?: string;
};

const ArrowUpRightIcon = ({
  fillColor = "var(--color-icon-black)",
  fillColorBackground = "var(--color-icon-white)",
  viewBox = "0 0 40 40",
  className,
}: ArrowUpRightIconProps) => {
  return (
    <svg
      viewBox={viewBox}
      fill={fillColor}
      className={className}
      xmlns="http://www.w3.org/2000/svg"
    >
      <rect width="40" height="40" rx="20" fill={fillColorBackground} />
      <path
        d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z"
        fill={fillColor}
      />
    </svg>
  );
};

export default ArrowUpRightIcon;