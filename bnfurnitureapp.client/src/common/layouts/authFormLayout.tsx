import styles from "./authFormLayout.module.scss";

interface FormLayoutProps {
  children: React.ReactNode;
  isLoading: boolean;
  handleSubmit: () => void;
  buttonText: string;
  imageSrc: string;
  buttonClass: string;
  imageHeading?: string;
  imageHeadingColor?: string;
  afterSubmitButtonSection?: React.ReactNode;
}

const FormLayout: React.FC<FormLayoutProps> = ({
  children,
  isLoading,
  handleSubmit,
  buttonText,
  imageSrc,
  buttonClass,
  imageHeading,
  imageHeadingColor,
  afterSubmitButtonSection,
}) => {
  return (
    <div className={styles.container}>
      <div className={styles["image-container"]}>
        <div className={`${styles['image-heading']} ${imageHeadingColor}`}>{imageHeading}</div>
        <img src={imageSrc} alt="Form" />
      </div>
      <form onSubmit={handleSubmit} className={styles["form-container"]}>
        {children}
        <button type="submit" disabled={isLoading} className={buttonClass}>
          {buttonText}
        </button>
        {afterSubmitButtonSection}
      </form>
    </div>
  );
};

export default FormLayout;
