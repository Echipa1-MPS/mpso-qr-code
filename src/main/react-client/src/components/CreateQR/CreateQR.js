import { useEffect, useState, useContext } from "react";
import { ThemeContext } from "../../App";

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
                        <div className="flex-container-row">
                            <div>Alege materia</div>
                            <div class="dropdown show">
                                <a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    Materia
                                </a>

                                <div class="dropdown-menu" aria-labelledby="dropdownMenuLink">
                                    <a class="dropdown-item" href="#">Action</a>
                                    <a class="dropdown-item" href="#">Another action</a>
                                    <a class="dropdown-item" href="#">Something else here</a>
                                </div>
                            </div>
                        </div>
                        <div>Alege intervalul</div>
                        <div>Durata QR</div>
                        <div>Repetare</div>
                        <div className="white-text-font
                                        create-qr-button-style 
                                        create-qr-button-width"
                            style={{backgroundColor: theme.rose_budget}}>Submit</div>
                    </div>

                </div>
                <div className="cornsilk-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">'
                </div>
            </div>
        </div>);
}