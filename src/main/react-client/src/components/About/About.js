import React, { Component} from 'react';
import '../../App.css';
import stefan_n from '../../images/avatars/stefan_n.png';
import stefan_p from '../../images/avatars/stefan_p.png';
import cristi_s from '../../images/avatars/cristi_s.png';
import tudor_c from '../../images/avatars/tudor_c.png';

export default class About extends Component {
    render() {
        return (
            <div className="flex-container-row-center full-height-vw">
                <div className="flex-container-column-center">
                    <h2 className="center-text">Authors</h2>
                    <div className="flex-container-row">
                        <div>
                            <img src={stefan_n} alt="Stefan Nedelcu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                        </div>
                        <div>
                            <img src={stefan_p} alt="Stefan Popa" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                        </div>
                        <div>
                            <img src={cristi_s} alt="Cristi Sandu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                        </div>
                        <div>
                            <img src={tudor_c} alt="Tudor Calafateanu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}