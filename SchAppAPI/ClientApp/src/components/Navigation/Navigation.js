import React from 'react'
import { Link } from 'react-router-dom'

function Navigation() {
    return (
        <div className="container-fluid">
            <div id="page-topbar">
                <div className="navbar-header" style={{background:"#184C35"}}>
                    <div style={{paddingLeft:"90px"}}>
                        <img src="assets/images/missionLogo.svg"  alt="logo"/>
                    </div>
                    
                    <div className="container-fluid" >
                        <div className="float-right">
                            <div className="dropdown d-inline-block d-lg-none ml-2">
                                <button type="button" className="btn header-item noti-icon waves-effect" id="page-header-search-dropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i className="mdi mdi-magnify"></i>
                                </button>
                                <div className="dropdown-menu dropdown-menu-lg dropdown-menu-right p-0" aria-labelledby="page-header-search-dropdown">
                                    <form className="p-3">
                                        <div className="form-group m-0">
                                            <div className="input-group">
                                                <input type="text" className="form-control" placeholder="Search ..." aria-label="Recipient's username"></input>
                                                <div className="input-group-append">
                                                    <button className="btn btn-primary" type="submit"><i className="mdi mdi-magnify"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>
                            </div>

                            <div className="dropdown d-inline-block">
                                <button type="button" className="btn header-item noti-icon waves-effect" id="page-header-notifications-dropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i className="mdi mdi-bell-outline" style={{color:"#fff"}}></i>
                                    <span className="badge badge-danger badge-pill">3</span>
                                </button>
                                <div className="dropdown-menu dropdown-menu-lg dropdown-menu-right p-0" aria-labelledby="page-header-notifications-dropdown">
                                    <div className="p-3">
                                        <div className="row align-items-center">
                                            <div className="col">
                                                <h6 className="m-0"> Notifications </h6>
                                            </div>
                                            <div className="col-auto">
                                                <a href="#!" className="small"> View All</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div data-simplebar style={{maxHeight:"230px"}}>
                                        <a href="" className="text-reset notification-item">
                                            <div className="media">
                                                <div className="avatar-xs mr-3">
                                                    <span className="avatar-title bg-primary rounded-circle font-size-16">
                                                        <i className="bx bx-cart"></i>
                                                    </span>
                                                </div>
                                                <div className="media-body">
                                                    <h6 className="mt-0 mb-1">Your order is placed</h6>
                                                    <div className="font-size-12 text-muted">
                                                        <p className="mb-1">If several languages coalesce the grammar</p>
                                                        <p className="mb-0"><i className="mdi mdi-clock-outline"></i> 3 min ago</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                        <a href="" className="text-reset notification-item">
                                            <div className="media">
                                                <img src="assets/images/users/avatar-3.jpg" className="mr-3 rounded-circle avatar-xs" alt="user-pic" />
                                                <div className="media-body">
                                                    <h6 className="mt-0 mb-1">James Lemire</h6>
                                                    <div className="font-size-12 text-muted">
                                                        <p className="mb-1">It will seem like simplified English.</p>
                                                        <p className="mb-0"><i className="mdi mdi-clock-outline"></i> 1 hours ago</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                        <a href="" className="text-reset notification-item">
                                            <div className="media">
                                                <div className="avatar-xs mr-3">
                                                    <span className="avatar-title bg-success rounded-circle font-size-16">
                                                        <i className="bx bx-badge-check"></i>
                                                    </span>
                                                </div>
                                                <div className="media-body">
                                                    <h6 className="mt-0 mb-1">Your item is shipped</h6>
                                                    <div className="font-size-12 text-muted">
                                                        <p className="mb-1">If several languages coalesce the grammar</p>
                                                        <p className="mb-0"><i className="mdi mdi-clock-outline"></i> 3 min ago</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </a>
                                    </div>
                                    <div className="p-2 border-top">
                                        <a className="btn btn-sm btn-link font-size-14 btn-block text-center" href="javascript:void(0)">
                                            <i className="mdi mdi-arrow-right-circle mr-1"></i> View More..
                                        </a>
                                    </div>
                                </div>
                            </div>

                            <div className="dropdown d-inline-block">
                                <button type="button" className="btn header-item waves-effect" id="page-header-user-dropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <img className="rounded-circle header-profile-user" src="assets/images/users/avatar-2.jpg" alt="Header Avatar" />
                                    <span className="d-none d-xl-inline-block ml-1" style={{color:"#fff"}}>Admin</span>
                                    <i className="mdi mdi-chevron-down d-none d-xl-inline-block"></i>
                                </button>
                                <div className="dropdown-menu dropdown-menu-right">
                                    <a className="dropdown-item" href="#"><i className="bx bx-user font-size-16 align-middle mr-1"></i> Profile</a>
                                    <a className="dropdown-item d-block" href="#"><span className="badge badge-success float-right">11</span><i className="bx bx-wrench font-size-16 align-middle mr-1"></i> Settings</a>
                                    <a className="dropdown-item" href="#"><i className="bx bx-lock-open font-size-16 align-middle mr-1"></i> Lock screen</a>
                                    <div className="dropdown-divider"></div>
                                    <a className="dropdown-item text-danger" href="#"><i className="bx bx-power-off font-size-16 align-middle mr-1 text-danger"></i> Logout</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="vertical-menu"  >
                <div className="h-100">
                    <div className="user-wid text-center py-4">
                        <div className="user-img">
                            <img src="assets/images/users/avatar-2.jpg" alt="" className="avatar-md mx-auto rounded-circle" />
                        </div>

                        <div className="mt-3">

                            <a href="#" className="text-dark font-weight-medium font-size-16">Super Admin</a>
                            <p className="text-body mt-1 mb-0 font-size-13">ihs admin</p>

                        </div>
                    </div>

                    <div id="sidebar-menu">
                        <ul className="metismenu list-unstyled" id="side-menu">
                            <li className="menu-title">Menu</li>

                            <li>
                                <a href="javascript: void(0);" className="waves-effect">
                                    <i className="mdi mdi-airplay"></i>
                                    <span>Overview</span>
                                </a>
                            </li>

                            <li>
                                <Link to="/teacher" className="waves-effect">
                                    <i className="mdi mdi-flip-horizontal"></i>
                                    <span>Teachers</span>
                                </Link>
                            </li>

                            <li>
                                <a href="calendar.html" className=" waves-effect">
                                    <i className="mdi mdi-calendar-text"></i>
                                    <span>Subjects</span>
                                </a>
                            </li>

                            <li>
                                <a href="javascript: void(0);" className="waves-effect">
                                    <i className="mdi mdi-calendar-check"></i>
                                    <span>Lessons</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript: void(0);" className="waves-effect">
                                    <i className="mdi mdi-calendar-check"></i>
                                    <span>Class</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript: void(0);" className="waves-effect">
                                    <i className="mdi mdi-calendar-check"></i>
                                    <span>Roles</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript: void(0);" className="waves-effect">
                                    <i className="mdi mdi-inbox-full"></i>
                                    <span>Chats</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Navigation
