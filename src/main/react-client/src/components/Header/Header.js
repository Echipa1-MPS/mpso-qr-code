import React, { Component } from 'react';
import '../../App.css';

export default class Header extends Component {
    render() {
        return(
            <nav className="navbar navbar-expand-lg light-blue-background white-text-font">
                <span className="navbar-brand">QR Scanner</span>
                <button className="navbar-toggler navbar-light" 
                        type="button" 
                        data-toggle="collapse" 
                        data-target="#navbarNavAltMarkup" 
                        aria-controls="navbarNavAltMarkup" 
                        aria-expanded="false" 
                        aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNavAltMarkup" style={{flexDirection: "row-reverse"}}>
                    <div className="navbar-nav">
                        <a className="nav-item nav-link header-options-text-size white-text-font" href="/">Home</a>
                        <a className="nav-item nav-link header-options-text-size white-text-font" href="/login">Login</a>
                        <a className="nav-item nav-link header-options-text-size white-text-font" href="/about">About</a>
                        {
                            this.props.loggedIn ? <a className="nav-item nav-link header-options-text-size white-text-font" href="/about">Logout</a> : ""
                        }
                    </div>
                </div>
            </nav>
        )
    }
}