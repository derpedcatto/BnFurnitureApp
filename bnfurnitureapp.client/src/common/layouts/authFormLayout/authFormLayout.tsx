import styles from "./authFormLayout.module.scss";

interface AuthFormLayoutProps {
  children: React.ReactNode;
  imageSrc: string;
  imageHeading?: string;
  imageHeadingColor?: string;
  afterFormSection?: React.ReactNode;
}

const AuthFormLayout: React.FC<AuthFormLayoutProps> = ({
  children,
  imageSrc,
  imageHeading,
  imageHeadingColor,
  afterFormSection,
}) => {
  return (
    <div className={styles.container}>
      <div className={styles["image-container"]}>
        <div className={`${styles["image-heading"]} ${imageHeadingColor}`}>
          {imageHeading}
        </div>
        <img src={imageSrc} alt="Form" />
      </div>
      <div className={styles["form-container"]}>
        {children}
        {afterFormSection}
      </div>
    </div>
  );
};

export default AuthFormLayout;
