import React, { useState, useEffect } from 'react'
import * as $ from 'jquery';
import 'summernote/dist/summernote-lite';
import 'summernote/dist/summernote-lite.css';
import Gallery from './Gallery/Gallery';


function SummerCodeEditor() {
    const [showGallery, setGalleryVisibility] = useState(false);
    useEffect(() => {
        $('#summernote').summernote({
            placeholder: 'Type in lesson, do multimedias are inserted on a new line',
            width: 1000,
            height: 500,
            spellCheck: true,
            toolbar: [
                ['font', ['bold', 'clear']],
                //['insert', ['picture', 'video']],
                ['view', ['fullscreen']],
                ['mybutton', ['insert','save']]
            ],

            buttons: {
                save: saveButton,
                insert: insertButton
            }
        });
    });


    const saveButton = function (context) {
        var ui = $.summernote.ui;

        // create button
        var button = ui.button({
            contents: '<i class="fa fa-save"/> Save Lesson',
            //tooltip: 'hello',
            click: function () {
                // invoke insertText method with 'hello' on editor module.
                context.invoke('editor.insertText', 'saved');
            }
        });

        return button.render();   // return button as jquery object
    }
    const insertButton = function (context) {
        var ui = $.summernote.ui;

        // create button
        var button = ui.button({
            contents: '<i class="fa fa-plus"/> Insert Multimedia',
            //tooltip: 'hello',
            click: function () {
                // invoke insertText method with 'hello' on editor module.
                setGalleryVisibility(true);
            }
        });

        return button.render();   // return button as jquery object
    }

    const handleImageInsertion = (url) => {
        var HTMLstring = '<p></p>';
        $('#summernote').summernote('pasteHTML', HTMLstring);


        $('#summernote').summernote('insertImage', url)

        setGalleryVisibility(false);

    }
    const handleVideoInsertion = (url) => {


        var HTMLstring = `<p><iframe frameborder="0" src="${url}" width="640" height="360" </iframe></p>`;
        $('#summernote').summernote('pasteHTML', HTMLstring);        

        setGalleryVisibility(false);
    }
    const hideGallery = () => { setGalleryVisibility(false)}


    return (
        <>     
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">Summernote</h4>
                    <p className="card-title-desc">Super simple wysiwyg editor on bootstrap</p>
                    <div id="summernote" className="summernote"></div>
                    {showGallery ? <Gallery closeGallery={ hideGallery } insertImage={handleImageInsertion} insertVideo={ handleVideoInsertion }/> : null}
                </div>
            </div>
        </>
    )
}

export default SummerCodeEditor
