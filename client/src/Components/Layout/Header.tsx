import React from "react";
import {NavLink} from "react-router-dom";

let logo = require("../../Assets/Images/tastehaven.jpg");

function Header() {
    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container-fluid">
                <img src={logo} style={{height: "40px"}} className="m-1 rounded-circle"/>
                <button className="navbar-toggler"
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent"
                        aria-expanded="false"
                        aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <NavLink className="nav-link active" aria-current="page" to='/'>Home</NavLink>
                        </li>


                    </ul>

                </div>
            </div>
        </nav>
    )
}

export default Header;