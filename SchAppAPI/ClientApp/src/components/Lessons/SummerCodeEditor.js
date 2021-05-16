import React, { useState, useEffect } from 'react'
import * as $ from 'jquery';
import 'summernote/dist/summernote-lite';
import 'summernote/dist/summernote-lite.css';


var saveButton = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '<i class="fa fa-save"/> Save',
        //tooltip: 'hello',
        click: function () {
            // invoke insertText method with 'hello' on editor module.
            context.invoke('editor.insertText', 'saved');
        }
    });

    return button.render();   // return button as jquery object
}
var insertButton = function (context) {
    var ui = $.summernote.ui;

    // create button
    var button = ui.button({
        contents: '<i class="fa fa-plus"/> Insert Multimedia',
        //tooltip: 'hello',
        click: function () {
            // invoke insertText method with 'hello' on editor module.
            context.invoke('editor.insertText', 'Inserted');
        }
    });

    return button.render();   // return button as jquery object
}


function SummerCodeEditor() {

    useEffect(() => {
        $('#summernote').summernote({
            placeholder: 'Introduction of the lesson',
            width: 1000,
            height: 500,
            spellCheck: true,
            toolbar: [
                ['font', ['bold', 'clear']],
                //['insert', ['picture', 'video']],
                ['view', ['fullscreen']],
                ['mybutton', ['save', 'insert']]
            ],

            buttons: {
                save: saveButton,
                insert: insertButton
            }
        });
    });

    return (
        <>     
            <div className="card">
                <div className="card-body">
                    <h4 className="card-title">Summernote</h4>
                    <p className="card-title-desc">Super simple wysiwyg editor on bootstrap</p>

                    <div id="summernote" className="summernote">Introduction</div>
                </div>
            </div>
        </>
    )
}

export default SummerCodeEditor
