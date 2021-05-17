import React, { useState, useEffect} from 'react'
import  $ from 'jquery';

import './Gallery.css';



function Gallery({insertImage, insertVideo, closeGallery}) {
  
    const [mediaSet, setMedia] = useState(
        [
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Security",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Picsum",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://www.youtube.com/watch?v=BJFdC7963jU",
                Thumbnail: "https://picsum.photos/200/200",
                Title: "Picsum",
                ContentType: "Video"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Security",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Picsum",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://www.youtube.com/watch?v=BJFdC7963jU",
                Thumbnail: "https://picsum.photos/200/200",
                Title: "Picsum",
                ContentType: "Video"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Security",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Picsum",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://www.youtube.com/watch?v=BJFdC7963jU",
                Thumbnail: "https://picsum.photos/200/200",
                Title: "Picsum",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",

                Title: "Security",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://picsum.photos/200/300",
                Thumbnail: "https://picsum.photos/200/200",
                Title: "Picsum",
                ContentType: "Image"
            },
            {
                MediaUrl: "https://www.youtube.com/watch?v=BJFdC7963jU",
                Thumbnail: "https://picsum.photos/200/200",
                Title: "Picsum",
                ContentType: "Video"
            }
        ]);
    const [currentSelectionIndex, mediaSelectedUpdateIndex] = useState('-1');


    const getMedia = () => {
        return mediaSet.map((media, index) => {
            const selected = index == currentSelectionIndex;

            return <div key={index}  style={{ textAlign: 'center' }} className={"col-sm-4" + (selected ? " imageSelected" : "")} onClick={() => { mediaSelectedUpdateIndex(index)}}>
               <img className="img-thumbnail" src={media.Thumbnail}/>
               <p>
                    {media.ContentType }: { media.Title}
               </p>
            </div>
        });
    }
    
    // When the user clicks on <span> (x), close the modal
    const closeModal = () => {
        closeGallery();
    }

    const mediaSelected = () => {
        if (currentSelectionIndex == "-1") closeGallery();
        const selectedMedia = mediaSet[currentSelectionIndex];
        if (selectedMedia.ContentType == "Image") {
            insertImage(selectedMedia.MediaUrl);
        }
        else if (selectedMedia.ContentType == "Video") {
            insertVideo(selectedMedia.MediaUrl);
        }
        
    }
    return (
        
            <div id="myModal" className="modal">

              <div className="modal-content">
                    <div className="modal-header">
                        <h2>Gallery</h2>

                        <span className="close" onClick={ closeModal }>&times;</span>
                    </div>
                    <div className="modal-body">
                        <div className="row">

                            { getMedia()}
                            </div>
                    </div>
                    <div className="modal-footer">
                        <h3>
                            <button style={{ background: "#184C35", color: "#fff" }} className="btn btn-rounded">Upload</button>
                        </h3>
                        <h3>
                            <button style={{ background: "#184C35", color: "#fff" }} className="btn btn-rounded" onClick={mediaSelected }>Insert</button>
                        </h3>
                    </div>
                </div>

            </div>

        
    )
}

export default Gallery
