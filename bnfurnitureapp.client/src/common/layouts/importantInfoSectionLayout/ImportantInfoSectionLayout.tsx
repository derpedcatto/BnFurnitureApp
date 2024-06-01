import styles from './ImportantInfoSectionLayout.module.scss';

type ImportantInfoSectionLayoutProps = {
  children: React.ReactNode;
  headingText: string;
  imageSrc: string;
}

const ImportantInfoSectionLayout: React.FC<ImportantInfoSectionLayoutProps> = ({
  children,
  headingText,
  imageSrc
}) => {
  return (
    <div className={styles.container}>
      <div className={styles['image-container']}>
        <img src={imageSrc} />
      </div>
      <div className={styles['container-info']}>
        <div className={styles['info-heading']}>{headingText}</div>
        <div className={styles['info-content']}>{children}</div>
      </div>
    </div>
  )
}

export default ImportantInfoSectionLayout;