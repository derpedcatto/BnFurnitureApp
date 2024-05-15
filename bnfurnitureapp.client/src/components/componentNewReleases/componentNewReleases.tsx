import React from 'react';
import "./componentNewReleases.css"

interface Props {
  // необхідні властивості
}

const NewReleases: React.FC<Props> = () => {
  return (
    <div>
       <div className='newReleasesRow'>
            <div className='newRelease-1'></div>
            <div className='newRelease-2'></div>
        </div>
        <div className='newReleasesRow'>
            <div className='newRelease-3'></div>
            <div className='newRelease-4'></div>
        </div>    
    </div>
  );
};

export default NewReleases;