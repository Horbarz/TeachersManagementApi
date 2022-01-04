import axios from 'axios'
import React,{useState,useEffect} from 'react'
import Navigation from '../Navigation/Navigation'
import {apiUrl} from '../../config'
import MUIDataTable from "mui-datatables";
import { Link } from 'react-router-dom';

function Teacher() {

    const columns = [
        {
         name: "firstName",
         label: "First Name",
         options: {
          filter: false,
          sort: true,
         }
        },
        {
         name: "lastName",
         label: "Last Name",
         options: {
          filter: false,
          sort: true,
         }
        },
        {
         name: "gender",
         label: "Gender",
         options: {
          filter: true,
          sort: false,
         }
        },
        {
         name: "phoneNumber",
         label: "Phone Number",
         options: {
          filter: false,
          sort: false,
         }
        },
        {
        name: "email",
        label: "Email",
        options: {
            filter: false,
            sort: false,
        }
        },
        {
        name: "cateory",
        label: "Category",
        options: {
            filter: true,
            sort: false,
        }
        },
        {
        name: "experience",
        label: "Teacher Exp",
        options: {
            filter: true,
            sort: true,
        }
        },
    ];
    
    const options = {
        filterType: 'checkbox',
      };

    let header = {
        headers: {
            'Access-Control-Allow-Origin': '*'
        }
      };

    const [allTeachers, setAllTeachers] = useState([])

    const getAllTeachers = () => {
        var teacherUrl = apiUrl+"teachers/getAll"
        axios.get(teacherUrl,header)
            .then(res => {
                setAllTeachers(res.data)
            })
            .catch(e => {
                console.log(e)
        })
    }

    useEffect(() => {
        getAllTeachers()
    },[])

    return (
        <>
        <Navigation />
        <div className="main-content">
            <div className="page-content">
                <div class="row">
                    <div class="col-12">
                        <div class="page-title-box d-flex align-items-center justify-content-between">
                            <h4 class="page-title mb-0 font-size-18">Teacher Management</h4>

                            <div class="page-title-right">
                                <Link to="/create_teacher">
                                    <button style={{background:"#184C35", color:"#fff"}} className="btn btn-rounded">Create Teacher</button>
                                </Link>
                            </div>

                        </div>
                    </div>
                </div>

                <div className="row">
                    <div className="col-12">
                            <MUIDataTable
                                title={"All Teachers"}
                                data={allTeachers}
                                columns={columns}
                                options={options}
                                className ="table table-striped"
                            />
                    </div>
                </div>
            </div>
        </div>
        </>
    )
}

export default Teacher
