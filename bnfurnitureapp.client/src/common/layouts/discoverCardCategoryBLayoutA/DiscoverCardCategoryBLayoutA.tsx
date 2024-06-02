import styles from './DiscoverCardCategoryBLayoutA.module.scss';

interface DiscoverCardCategoryBLayoutAProps {
  children: React.ReactNode;
}

const DiscoverCardCategoryBLayoutA: React.FC<DiscoverCardCategoryBLayoutAProps> = ({
  children
}) => {
  return (
    <div className={styles.container}>
      {children}
    </div>
  )
}

export default DiscoverCardCategoryBLayoutA;