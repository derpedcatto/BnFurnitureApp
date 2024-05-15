import './componentCardHome.css'
const CardHome = () => {
    const items = [
        { id: 1, text: 'Подушки спальна кімната... 12$' },
        { id: 2, text: 'Подушки спальна кімната... 12$' },
        { id: 3, text: 'Подушки спальна кімната... 12$' }
    ];

    return (
        <div className='colmhomcard'>
            {items.map(item => (
                <div key={item.id} className="component15">     
                    <div className="Group58">
                        <div className="frame16">
                            <div className="top">Top</div>
                        </div>
                        <div className="group2">
                            <div className="Rectangle26"></div>
                            <div className="frame13">
                                <div className="bnText">{item.text}</div>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default CardHome;
