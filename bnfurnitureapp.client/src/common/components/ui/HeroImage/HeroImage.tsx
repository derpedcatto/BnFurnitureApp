import styles from './HeroImage.module.scss'

interface HeroImageProps {
  imageSrc: string;
}

const HeroImage: React.FC<HeroImageProps> = ({ imageSrc }) => {
  return (
    <div className={styles['hero-image-container']}>
      <img src={imageSrc} />
    </div>
  )
}

export default HeroImage;