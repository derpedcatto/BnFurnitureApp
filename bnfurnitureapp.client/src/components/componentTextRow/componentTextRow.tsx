import './componentTextRow.css'

const TextRow = () => {
  return (
    <div className='Frame20row'>
      {[...Array(8)].map((_, index) => (
        <div className='Frame21' key={index}>
          <div className='Frame21Text'>назва набору</div>
        </div>
      ))}
    </div>
  );
};

export default TextRow;
