import React, { Component} from 'react';
import '../../App.css';
import stefan_n from '../../images/avatars/stefan_n.png';
import stefan_p from '../../images/avatars/stefan_p.png';
import cristi_s from '../../images/avatars/cristi_s.png';
import tudor_c from '../../images/avatars/tudor_c.png';
import maria_c from '../../images/avatars/maria_c.png';
import stela_j from '../../images/avatars/stela_j.png';

export default class About extends Component {
    render() {
        return (
            <div className="flex-container-row-center full-height-vw">
                <div className="flex-container-column-center" style={{maxWidth: "1000px", paddingBottom: "200px"}}>
                    <h2 className="center-text">Authors</h2>
                    <div className="flex-container-row" style={{justifyContent: "inherit"}}>
                        <div className="authors-cell">
                            <img src={stefan_n} alt="Stefan Nedelcu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Stefan Nedelcu</p>
                        </div>
                        <div className="authors-cell">
                            <img src={stefan_p} alt="Stefan Popa" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Stefan Popa</p>
                        </div>
                        <div className="authors-cell">
                            <img src={cristi_s} alt="Cristi Sandu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Cristi Sandu</p>
                        </div>
                        <div className="authors-cell">
                            <img src={tudor_c} alt="Tudor Calafateanu" 
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Tudor Calafateanu</p>
                        </div>
                        <div className="authors-cell">
                            <img src={maria_c} alt="Maria Carmina"
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Maria Carmina</p>
                        </div>
                        <div className="authors-cell">
                            <img src={stela_j} alt="Stela Josan"
                                className="avatar rounded-avatar-image author-avatar-width avatar-margin avatar-border"/>
                            <p>Stela Josan</p>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}