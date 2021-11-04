import React, { Component } from 'react';
import '../../App.css';

export default class Header extends Component {
    render() {
        return(
            <nav class="navbar navbar-expand-lg light-blue-background white-text-font">
                <span class="navbar-brand">QR Scanner</span>
                <button class="navbar-toggler navbar-light" 
                        type="button" 
                        data-toggle="collapse" 
                        data-target="#navbarNavAltMarkup" 
                        aria-controls="navbarNavAltMarkup" 
                        aria-expanded="false" 
                        aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavAltMarkup" style={{flexDirection: "row-reverse"}}>
                    <div class="navbar-nav">
                        <span class="nav-item nav-link header-options-text-size" href="#">Home</span>
                        <span class="nav-item nav-link header-options-text-size" href="#">Login</span>
                    </div>
                </div>
            </nav>
        )
    }
}