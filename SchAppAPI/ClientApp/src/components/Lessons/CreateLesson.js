import React, { useState } from 'react'
import Navigation from '../Navigation/Navigation'
import { apiUrl } from '../../config'
import axios from 'axios'

import Popper from 'popper.js';
import $ from 'jquery';
import SummerCodeEditor from './SummerCodeEditor';

function CreateLesson() {



    return (
        <>
            <Navigation />
            <div className="main-content">
                <div className="page-content">
                    <div className="row">
                        <div className="col-12">
                            <div className="page-title-box d-flex align-items-center justify-content-between">
                                <h4 className="page-title mb-0 font-size-18">Create Lesson</h4>
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-12">
                            <SummerCodeEditor/>
                        </div>
                    </div>

                </div>
            </div>
        </>
    )
}

export default CreateLesson
