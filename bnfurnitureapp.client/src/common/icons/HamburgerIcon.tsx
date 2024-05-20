type HamburgerIconProps = {
  viewBox?: string;
  fillColor?: string;
  className?: string;
};

const HamburgerIcon = ({
  fillColor = "var(--text-gray-color)",
  viewBox = "0 0 24 24",
  className,
}: HamburgerIconProps) => {
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
        d="M3 18V16H21V18H3ZM3 13V11H21V13H3ZM3 8V6H21V8H3Z"
        fill={fillColor}
      />
    </svg>
  );
};

export default HamburgerIcon;
