import { useEffect, useState, useContext } from "react";
import { ThemeContext } from "../../App";
import QRCode from "react-qr-code";

export default function CreateQR() {

    const [courses, setCourses] = useState([]);
    const [courseIntervals, setCourseIntervals] = useState([]);
    const [qrDuration, setQrDuration] = useState(0);
    const [qrRepeats, setQrRepeats] = useState(0);

    const theme = useContext(ThemeContext);

    const submitQrSession = () => {}

    return (
        <div className="flex-container-row-center">
            <div className="flex-container-row qrsection-sections-margin-top">
                <div className="aliceblue-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">
                    <div className="flex-container-column-center">  
                        <div className="flex-container-column" >
                            <div>Alege materia</div>
                            <div className="dropdown show">
                                <span className="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Materie</span>

                                <div className="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                    {['apa', 'tuica'].map(item => <span className="dropdown-item">{item}</span>)}
                                </div>
                            </div>
                        </div>
                        <div className="flex-container-column" style={{marginTop: "20px"}}>
                            <div>Alege intervalul</div>
                            <div className="dropdown show">
                                <span className="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Intervalul</span>

                                <div className="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                    {['apa', 'tuica'].map(item => <span className="dropdown-item">{item}</span>)}
                                </div>
                            </div>
                        </div>
                        <div className="flex-container-row" style={{marginTop: "20px"}}>
                            <div>Durata QR</div>
                        </div>
                        <div className="flex-container-row" style={{marginTop: "20px"}}>
                            <div>Repetare</div>
                        </div>
                        <div className="white-text-font
                                        create-qr-button-style 
                                        create-qr-button-width"
                            style={{backgroundColor: theme.rose_budget}}>Submit</div>
                    </div>

                </div>
                <div className="cornsilk-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">'
                
                <QRCode value="hey" />
                </div>
            </div>
        </div>);
}

function CreateQrDropdown(title, items) {


    return (
        <div className="dropdown show">
            <span className="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                {title}
            </span>

            <div className="dropdown-menu" aria-labelledby="dropdownMenuLink">
                {items.map(item => <span className="dropdown-item">{item}</span>)}
            </div>
        </div>
    );
}