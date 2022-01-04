import React,{useState} from 'react'
import axios from 'axios'
import './Login.css'
import { Link, Redirect } from 'react-router-dom'
import {apiUrl} from '../../config'
import IhsAlert from '../Utils/IhsAlert'

function Login() {

    const initialValues = {
        Email:'',
        Password: ''
    }

    let axiosConfig = {
        headers: {
            'Access-Control-Allow-Origin': '*'
        }
    };

    const [info, setInfo] = useState(initialValues);
    const [successLogin, setSuccessLogin] = useState(false);
    const [failureLogin, setFailureLogin] = useState(false);
    const [dashBoardRedirect, setDashBoardRedirect] = useState(false)

    const handleInputChange = (e) => {
        const {name, value} = e.target
        const fieldValue =   {[name]: value}
        setInfo({  
            ...info,
            ...fieldValue 
        })
    }

    const handleSubmit = e =>{
        e.preventDefault()
        //console.log(info)
        const proxyUrl = apiUrl + "authenticate/login"
        
        axios.post(proxyUrl,info,axiosConfig)
            .then(res => {
                console.log(res)
                if(res.data.isLoggedIn){
                    setSuccessLogin(true)
                    var data={
                        isLoggedIn:res.data.isLoggedIn,
                        token:res.data.token,
                    }
                    // sessionStorage.setItem('userLoggedIn', res.data.isLoggedIn);
                    sessionStorage.setItem('user',JSON.stringify(data));
                    setDashBoardRedirect(true)
                    // window.location.reload(false)
                }
                else{
                    //setLoading(false)
                    setFailureLogin(true)
                }

            })
            .catch(e => {
                console.log(e)
                setFailureLogin(true)
                //setLoading(false)
            })
         
    }

    if(dashBoardRedirect){
        return <Redirect push to='/teacher' />
    }

    return (
        <div className="home__hero-section">
            <div className='rowwer home__hero-row'>
                <div className="column gradcolor">
                    <div className='home__hero-text-wrapper'>
                        <img src="assets/images/womenInTech.png" alt='TechWomen' className="home__hero-design" />
                        <div className='top-line'> Bring home the magic of camp with Mission-T Summer Training Program </div>
                        <p className="home__hero-subtitle">
                            The IHS Summer Training Program implemented by TechQuest STEM Academy is intended for students and Teachers. 
                        </p>
                    </div>
                </div>

                <div className="column signin_column">
                    {successLogin ? <IhsAlert error={false} message="Login Successful"/> : null}
                    {failureLogin ? <IhsAlert error={true} message="Invalid login details"/> : null}
                    <h4>Sign In</h4>
                    <h5>Welcome, Admin</h5>
                    <p> You can now login into your dashboard with your email and password.</p>

                    <div>
                        <form className="form-horizontal" onSubmit={handleSubmit}>
                            <div className="form-group">   
                                <input type="text" className="form-control" id="email" name="Email" placeholder="Email" 
                                    value={info.Email} onChange={handleInputChange} required="Email is required"/>
                            </div>
                            <div className="form-group">   
                                <input type="password" className="form-control" id="password" name="Password" value={info.Password} 
                                    placeholder="Password" onChange={handleInputChange} required="Password is required"/>
                            </div>
                            <div className="mt-3">
                                <button style={{color:'#fff'}} className="btn btn-pri btn-block waves-effect waves-light" type="submit">Sign In</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login
