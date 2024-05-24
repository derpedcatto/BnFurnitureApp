import  { useEffect, useState } from 'react';
import './CardHome.css'

interface Item {
    product: string;
    category: string;
    price: string;
    url: string;
}

const CardHome = () => {
    const [items, setItems] = useState<Item[]>([]);
    const [, setMessage] = useState<string>('');
    const [, setError] = useState<string>('');

    useEffect(() => {
        fetch('/data.json')
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                if (data.isSuccess && data.statusCode === 200) {
                    setItems(data.data || []);
                    setMessage(data.message);
                } else {
                    setError(data.errors || 'Unknown error');
                }
            })
            .catch(error => {
                console.error('Error fetching data:', error);
                setError(error.message);
            });
    }, []);

    return (
        <div className='card-container'>
            {items.map(item => (
                <div className="card" key={item.product}>     
                    <div className="card-content">
                        <div className="card-badge">
                            <div className="badge-text">Top</div>
                        </div>
                        <div className="card-image-container">
                            <div className="card-image" style={{ backgroundImage: `url(${item.url})` }}></div>
                            <div className="card-details">
                                <div className="product-name">{item.product}</div>
                                <div className="product-category">{item.category}</div>
                                <div className="product-price">{item.price}</div>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default CardHome;





