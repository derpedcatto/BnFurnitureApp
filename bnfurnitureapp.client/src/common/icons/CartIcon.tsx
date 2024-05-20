type CartIconProps = {
  viewBox?: string;
  fillColor?: string;
  className?: string;
};

const CartIcon = ({
  fillColor = "var(--text-gray-color)",
  viewBox = "0 0 22 22",
  className,
}: CartIconProps) => {
  return (
    <svg
      viewBox={viewBox}
      fill="none"
      className={className}
      xmlns="http://www.w3.org/2000/svg"
    >
      <path
        fillRule="evenodd"
        clipRule="evenodd"
        d="M1 7.75H2.11111M2.11111 7.75L3.22222 19H18.7778L19.8889 7.75M2.11111 7.75H6.55556M21 7.75H19.8889M19.8889 7.75H15.4444M15.4444 7.75H6.55556M15.4444 7.75V5.5C15.4444 4.00038 14.5556 1 11 1C7.44444 1 6.55556 4.00038 6.55556 5.5V7.75M11 12.25V14.5M14.3333 12.25V14.5M7.66667 12.25V14.5"
        stroke={fillColor}
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
};

export default CartIcon;
