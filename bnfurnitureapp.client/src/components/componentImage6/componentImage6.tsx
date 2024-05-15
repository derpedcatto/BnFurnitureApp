import React from 'react';
import './componentImage6.css'

const ImageColumn: React.FC<{ className: string, horizontal: string, vertical: string }> = ({ className, horizontal, vertical }) => {
    return (
        <div className={className}>
            <div className={`image horizontal ${horizontal}`}>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white" />
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black" />
                </svg>
            </div>
            <div className={`image vertical ${vertical}`}>
                <svg className='svg-overlay' width="40" height="40" viewBox="0 0 40 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="40" height="40" rx="20" fill="white" />
                    <path d="M12.9385 29L26.2308 15.7077V27.6154H29V11H12.3846V13.7692H24.2923L11 27.0615L12.9385 29Z" fill="black" />
                </svg>
            </div>
        </div>
    );
};

const ImageColumns: React.FC = () => {
    return (
        <div className='image-columns'>
            <ImageColumn className='column' horizontal='horizontal1' vertical='vertical1' />
            <ImageColumn className='column' horizontal='horizontal2' vertical='vertical2' />
            <ImageColumn className='column' horizontal='horizontal3' vertical='vertical3' />
        </div>
    );
};

export default ImageColumns;

