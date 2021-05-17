import React,{useState} from 'react'
import Navigation from '../Navigation/Navigation'
import {apiUrl} from '../../config'
import axios from 'axios'

function CreateTeacher() {

    const initialValues = {
        email: '',
        firstName: '',
        lastName: '',
        gender: 0,
        schoolName: "",
        category: '',
        phoneNumber:'',
        location:{
            state:'',
            city:'',
        },
        experience: '',
        
    }

    const[teacher, addTeacher] = useState(initialValues)
    const[teacherCreated,setTeacherCreated] = useState(false)

    const handleInputChange = e => {
        const {name,value} = e.target
        const fieldValues = {[name]:value}
        addTeacher({
            ...teacher,
            ...fieldValues
        })
    }

    let headerConfig = {
        headers: {
            'Access-Control-Allow-Origin': '*'
        }
    };

    const handleSubmit = e => {
        e.preventDefault()
        console.log(teacher)
        const addUrl = apiUrl+"teachers/add"
        axios.post(addUrl,teacher)
            .then(res => {
                if(res.data.isRegistered){
                    //teacher added successfully
                    console.log(res)
                    setTeacherCreated(true)
                }
            })
            .catch(e => {
                console.log(e)
            })

    }

    
    return (
        <>
        <Navigation />
            <div className="main-content">
                <div className="page-content">
                    <div className="row"> 
                        <div class="col-12">
                            <div class="page-title-box d-flex align-items-center justify-content-between">
                                <h4 class="page-title mb-0 font-size-18">Create Teacher</h4>
                            </div>
                        </div>
                    </div>

                    <div className="row">
                        <div className="col-12">
                            <div className="card" >
                                <div className="card-body">
                                    <h4 className="card-title">Personal details</h4>
                                    <form className="needs-validation" onSubmit={handleSubmit}>
                                        <div className="row">
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom01">First name</label>
                                                    <input type="text" class="form-control" id="validationCustom01" placeholder="First name" 
                                                            name="firstName" value={teacher.firstName} onChange={handleInputChange} required />
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">Last name</label>
                                                    <input type="text" class="form-control" id="validationCustom02" placeholder="Last name" 
                                                        name="lastName" value={teacher.lastName} onChange={handleInputChange} required/>
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">Email</label>
                                                    <input type="text" class="form-control" id="validationCustom02" placeholder="Email" 
                                                    name="email" value={teacher.email} onChange={handleInputChange} required/>
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">Gender</label>
                                                    <select class="form-control select2" name="gender" value={teacher.gender} onChange={handleInputChange}>
                                                        <option value={+0}>Male</option>
                                                        <option value={+1}>Female</option>
                                                        <option value={+2}>Other</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="row">
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom01">Phone Number</label>
                                                    <input type="text" class="form-control" id="validationCustom01" placeholder="Phone Number" 
                                                        name="phoneNumber" value={teacher.phoneNumber} onChange={handleInputChange} required />
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">State</label>
                                                    <input type="text" class="form-control" id="validationCustom02" placeholder="State" 
                                                        name="state" value={teacher.state} onChange={handleInputChange} required/>
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">City</label>
                                                    <input type="text" class="form-control" id="validationCustom02" placeholder="City" 
                                                    name="city" value={teacher.city} onChange={handleInputChange} required/>
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 className="card-title" style={{marginTop:"50px"}}>Career Details</h4>
                                        <div className="row">
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom01">School Name</label>
                                                    <input type="text" class="form-control" id="validationCustom01" placeholder="School Name" 
                                                        name="schoolName" value={teacher.schoolName} onChange={handleInputChange} required />
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">Experience</label>
                                                    <input type="text" class="form-control" id="validationCustom02" placeholder="Experience" 
                                                        name="experience" value={teacher.experience} onChange={handleInputChange} required/>
                                                    <div class="valid-feedback">
                                                        Looks good!
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm">
                                                <div class="form-group">
                                                    <label for="validationCustom02">Category</label>
                                                    <select class="form-control select2" name="category" value={teacher.category} onChange={handleInputChange}>
                                                        <option value="Private School">Private School</option>
                                                        <option value="Government School">Government School</option>
                                                        
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <button style={{background:"#184c35", color:"#fff", marginTop:"30px"}} className="btn" type="submit">Submit form</button>
                                    </form>
                                    {teacherCreated ? <div id="sa-position"></div> : null}
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </>
    )
}

export default CreateTeacher
