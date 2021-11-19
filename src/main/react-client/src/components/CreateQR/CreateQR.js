import { useEffect, useState, useContext } from "react";
import { ThemeContext } from "../../App";
import QRCode from "react-qr-code";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown'
import { getCoursesDetails } from "../../helpers/apicaller";

export default function CreateQR() {

    const [courses, setCourses] = useState([]);
    const [courseIntervals, setCourseIntervals] = useState([]);
    const [chosenCourse, setChosenCourse] = useState();
    const [chosenInterval, setChosenInterval] = useState();
    const [qrDuration, setQrDuration] = useState(0);
    const [qrRepeats, setQrRepeats] = useState(0);

    const theme = useContext(ThemeContext);

    useEffect(() => {
        const fetchCourses = async () => {
            const result = await getCoursesDetails();
            setCourses(result);
        }
        fetchCourses();
    }, []);


    const submitQrSession = () => {
        console.log("Afisam valorile");
        console.log(chosenCourse);
        console.log(chosenInterval);
        console.log(qrDuration);
        console.log(qrRepeats); 
    }

    return (
        <div className="flex-container-row-center">
            <div className="flex-container-row qrsection-sections-margin-top">
                <div className="aliceblue-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">
                    <div className="flex-container-column-center qr-section-container">  
                        <div className="flex-container-column" >
                            <div className="qr-input-header">Choose subject</div>
                            <DropdownButton
                                alignRight
                                title="Subject"
                                id="dropdown-menu-align-right"
                                onSelect={(e) => setChosenCourse(e)}
                                    >
                                        <Dropdown.Item eventKey="option-1">option-1</Dropdown.Item>
                                        <Dropdown.Item eventKey="option-2">option-2</Dropdown.Item>
                                        <Dropdown.Item eventKey="option-3">option 3</Dropdown.Item>
                            </DropdownButton>
                        </div>
                        <div className="flex-container-column" style={{marginTop: "20px"}}>
                            <div className="qr-input-header">Choose interval</div>
                            <DropdownButton
                                alignRight
                                title="Interval"
                                id="dropdown-menu-align-right"
                                onSelect={(e) => setChosenInterval(e)}
                                    >
                                        <Dropdown.Item eventKey="option-1">option-1</Dropdown.Item>
                                        <Dropdown.Item eventKey="option-2">option-2</Dropdown.Item>
                                        <Dropdown.Item eventKey="option-3">option 3</Dropdown.Item>
                            </DropdownButton>
                        </div>
                        <div className="flex-container-column" style={{marginTop: "20px"}}>
                            <div className="qr-input-header">QR Duration</div>
                            <input type="number"
                                value = {qrDuration}
                                onChange = {(e) => setQrDuration(e.target.value)}
                                className="form-control" 
                                id="qrDuration" 
                                aria-describedby="qrDuration" 
                                placeholder="Enter in seconds how much should the QR be available"/>
                        </div>
                        <div className="flex-container-column" style={{marginTop: "20px"}}>
                            <div className="qr-input-header">Repetition times</div>
                            <input type="number"
                                value = {qrRepeats}
                                onChange = {(e) => setQrRepeats(e.target.value)}
                                className="form-control" 
                                id="qrDuration" 
                                aria-describedby="qrDuration" 
                                placeholder="Enter in seconds how much should the QR be available"/>
                        </div>
                        <div className="white-text-font
                                        create-qr-button-style 
                                        create-qr-button-width"
                            onClick={submitQrSession}
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