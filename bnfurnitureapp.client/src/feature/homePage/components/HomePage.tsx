// import { useDispatch, useSelector } from "react-redux";
// import { AppDispatch } from '../../../app/store';
// import { useNavigate } from "react-router-dom";
import HeroSection from './HeroSection';
import CardGroupSetA from '../../../common/components/cardGroupSets/CardGroupSetA';
import styles from './HomePage.module.scss';
import CardCategoryASlider from '../../../common/components/cardGroupSets/CardCategoryASlider';
import CardCategoryA1_1 from '../assets/CardCategoryA-1/1.jpg'
import CardCategoryA1_2 from '../assets/CardCategoryA-1/2.jpg';
import CardCategoryA1_3 from '../assets/CardCategoryA-1/3.jpg';
import CardCategoryA1_4 from '../assets/CardCategoryA-1/4.jpg';
import CardCategoryA1_5 from '../assets/CardCategoryA-1/5.jpg';
import CardCategoryA1_6 from '../assets/CardCategoryA-1/6.jpg';
import CardCategoryA1_7 from '../assets/CardCategoryA-1/7.jpg';
import CardCategoryA1_8 from '../assets/CardCategoryA-1/8.jpg';
import CardCategoryA1_9 from '../assets/CardCategoryA-1/9.jpg';
import CardCategoryA1_10 from '../assets/CardCategoryA-1/10.jpg';
import CardCategoryA2_1 from '../assets/CardCategoryA-2/1.jpg'
import CardCategoryA2_2 from '../assets/CardCategoryA-2/2.jpg';
import CardCategoryA2_3 from '../assets/CardCategoryA-2/3.jpg';
import CardCategoryA2_4 from '../assets/CardCategoryA-2/4.jpg';
import CardCategoryA2_5 from '../assets/CardCategoryA-2/5.jpg';
import CardCategoryA2_6 from '../assets/CardCategoryA-2/6.jpg';
import CardCategoryA2_7 from '../assets/CardCategoryA-2/7.jpg';
import CardCategoryA2_8 from '../assets/CardCategoryA-2/8.jpg';
import CardCategoryA2_9 from '../assets/CardCategoryA-2/9.jpg';
import CardCategoryA2_10 from '../assets/CardCategoryA-2/10.jpg';

const HomePage = () => {
  // const dispatch = useDispatch<AppDispatch>();
  // const navigate = useNavigate();

  const dummyCategories = [
    { categoryName: 'Меблі для вітальні', imageSrc: CardCategoryA1_1, redirectTo: '/' },
    { categoryName: 'Меблі для спальні', imageSrc: CardCategoryA1_2, redirectTo: '/' },
    { categoryName: 'Кухонні меблі', imageSrc: CardCategoryA1_3, redirectTo: '/' },
    { categoryName: 'Офісні меблі', imageSrc: CardCategoryA1_4, redirectTo: '/' },
    { categoryName: 'Меблі для їдальні', imageSrc: CardCategoryA1_5, redirectTo: '/' },
    { categoryName: 'Ванна кімната', imageSrc: CardCategoryA1_6, redirectTo: '/' },
    { categoryName: 'Меблі для дитячої', imageSrc: CardCategoryA1_7, redirectTo: '/' },
    { categoryName: 'Текстиль', imageSrc: CardCategoryA1_8, redirectTo: '/' },
    { categoryName: 'Освітлення', imageSrc: CardCategoryA1_9, redirectTo: '/' },
    { categoryName: 'Декор та аксесуари', imageSrc: CardCategoryA1_10, redirectTo: '/' }
  ];

  const dummyProductSets = [
    { categoryName: 'Кухонний набір', imageSrc: CardCategoryA2_1, redirectTo: '/' },
    { categoryName: 'Спа-набір', imageSrc: CardCategoryA2_2, redirectTo: '/' },
    { categoryName: 'Набір постелі', imageSrc: CardCategoryA2_3, redirectTo: '/' },
    { categoryName: 'Набір меблів', imageSrc: CardCategoryA2_4, redirectTo: '/' },
    { categoryName: 'Набір посуду', imageSrc: CardCategoryA2_5, redirectTo: '/' },
    { categoryName: 'Набір для дітей', imageSrc: CardCategoryA2_6, redirectTo: '/' },
    { categoryName: 'Офісний набір', imageSrc: CardCategoryA2_7, redirectTo: '/' },
    { categoryName: 'Набір декору', imageSrc: CardCategoryA2_8, redirectTo: '/' },
    { categoryName: 'Набір текстилю', imageSrc: CardCategoryA2_9, redirectTo: '/' },
    { categoryName: 'Світильниковий набір', imageSrc: CardCategoryA2_10, redirectTo: '/' }
  ];

  return(
    <>
      <div className={styles.container}>
        <HeroSection />
        <div className={styles['home-body']}>
          <CardGroupSetA />
          <CardCategoryASlider
            sectionName='РЕКОМЕНДАЦІЇ'
            categories={dummyCategories}
          />
          <CardCategoryASlider
            sectionName='НАКРАЩІ НАБОРИ'
            categories={dummyProductSets}
          />
        </div>
      </div>
    </>
  );
}

/*
      <p>ЗНАЙДИ ТЕ, ЩО ШУКАЄШ!</p>
      <p>РЕКОМЕНДАЦІЇ</p>
      <p>НАЙКРАЩІ НАБОРИ</p>
      <p>НОВИНКИ</p>
      <p>КАТАЛОГ</p>
      <p>НАБОРИ</p>
      <p>ДОДАТКОВІ РЕКОМЕНДАЦІЇ</p>
      <p>ВАЖЛИВА ІНФОРМАЦІЯ</p>
*/

export default HomePage;