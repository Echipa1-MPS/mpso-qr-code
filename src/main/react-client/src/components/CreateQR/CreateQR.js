import { useEffect, useState, useContext } from "react";
import { ThemeContext } from "../../App";
import QRCode from "react-qr-code";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown'
import { 
    getCoursesDetails,
    postCreateQr,
    postUpdateQr
} from "../../helpers/apicaller";

export default function CreateQR() {

    const [coursesDetails, setCoursesDetails] = useState();
    const [courses, setCourses] = useState([]);
    const [courseIntervals, setCourseIntervals] = useState([]);
    const [chosenCourse, setChosenCourse] = useState();
    const [chosenInterval, setChosenInterval] = useState();
    const [chosenDuration, setChosenDuration] = useState(0);
    const [chosenRepeats, setChosenRepeats] = useState(0);
    const [randomQrInt, setRandomQrInt] = useState();
    const [receivedQrId, setReceivedQrId] = useState();
    const [finishedSession, setFinishedSession] = useState(false);
    //const [lockSubmitButton, setLockSubmitButton] = useState(false);

    const theme = useContext(ThemeContext);

    useEffect(() => {
        const fetchCourses = async () => {
            const result = await getCoursesDetails();
            setCoursesDetails(result);
            setCourses(result.map(function(item, index) {  return {index: index, id: item.id, title: item.title}; }));
            setCourseIntervals([]);
            setChosenCourse();
            setChosenInterval();
            setChosenDuration(0);
            setChosenRepeats(0);
        }
        fetchCourses();
    }, []);

    useEffect(() => {
        if (!chosenCourse || !chosenInterval)
            return;

        if (!receivedQrId) {
            postCreateQr({
                id_curs: chosenCourse.id_curs,
                id_interval: chosenInterval.id_interval,
                offset: chosenDuration,
                key: randomQrInt}, 
                succesfulQrSubmit, 
                failureQrSubmit);
        }
        else {
            postUpdateQr({
                id_curs: chosenCourse,
                key: randomQrInt}, 
                succesfulQrUpdate, 
                failureQrUpdate);
        }

    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [randomQrInt]);

    useEffect(() => {
        const regenerate = async () => {
            if (!chosenCourse || !receivedQrId || !chosenRepeats)
            return;

            await delay(chosenDuration * 1000);
            if (chosenRepeats > 1) 
                regenerateQrCode();
            else
                setFinishedSession(true);
        }
        regenerate();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [receivedQrId, chosenRepeats]);

    const hasChosenCourse = (course) => {
        setChosenCourse({
            index: course.split('&')[0],
            id_curs: course.split('&')[1],
            title: course.split('&')[2]
        });
        setCourseIntervals(coursesDetails[course.split('&')[0]].intervals);
        setChosenInterval();
        setChosenDuration(0);
        setChosenRepeats(0);
    }

    const hasChosenInterval = (interval) => {
        setChosenInterval({
            id_interval: interval.split('&')[0], 
            title: interval.split('&')[1]
        });
        setChosenDuration(0);
        setChosenRepeats(0);
    }

    const hasChosenDuration = (duration) => {
        setChosenDuration(Number(duration));
        setChosenRepeats(0);
    }

    const hasChosenRepeats = (repeats) => {
        setChosenRepeats(Number(repeats));
    }

    const generateRandomInt = () => {
        return Math.floor(Math.random() * 1000000);
    }

    const submitQrSession = () => {
        setRandomQrInt(generateRandomInt());
        //setLockSubmitButton(true);
    }

    const succesfulQrSubmit = (result) => {
        setReceivedQrId(result.data.id_qr);
    }

    const failureQrSubmit = (error) => {
        //setLockSubmitButton(false);
    }

    const succesfulQrUpdate = (result) => {
        setChosenRepeats(chosenRepeats - 1);
    }

    const failureQrUpdate = (error) => {
    }

    const delay = ms => new Promise(res => setTimeout(res, ms));

    const regenerateQrCode = async () => {
        setRandomQrInt(generateRandomInt());
    }

    const clearQrSession = () => {
        setChosenCourse();
        setChosenInterval();
        setChosenDuration(0);
        setChosenRepeats(0);
        setRandomQrInt();
        setReceivedQrId();
        setFinishedSession(false);
    }

    return (
        <div className="flex-container-row-center">
            <div className="flex-container-row qrsection-sections-margin-top">
                <div className="aliceblue-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">
                    <div className="flex-container-column-center qr-section-container">
                        <p style={{fontSize: "1.2rem", fontWeight: "bold"}}>Set up QR session</p>
                        <div className="flex-container-column" >
                            <div className="qr-input-header">Choose course: </div>
                            <div className="flex-container-row" style={{alignItems: "baseline"}}>
                                <DropdownButton
                                    title="Subject"
                                    id="dropdown-menu-align-right"
                                    onSelect={(e) => hasChosenCourse(e)}
                                        >
                                        { courses.map(course => <Dropdown.Item eventKey={course.index + "&" + course.id + "&" + course.title} key={course.index}>
                                            {course.title}</Dropdown.Item>) }
                                </DropdownButton>
                                { chosenCourse &&
                                    <p style={{marginLeft: "20px", fontSize: "1.1rem", color: "green"}}>
                                        You choose "{chosenCourse.title}"
                                    </p>
                                }
                            </div>
                        </div>
                        {chosenCourse ?
                            <div className="flex-container-column" style={{marginTop: "20px"}} >
                                <div className="qr-input-header">Choose interval: </div>
                                <div className="flex-container-row" style={{alignItems: "baseline"}}>
                                    <DropdownButton
                                        title="Interval"
                                        id="dropdown-menu-align-right"
                                        onSelect={(e) => hasChosenInterval(e)}
                                            >
                                            { courseIntervals.map(interval => <Dropdown.Item eventKey={interval.id + '&' + interval.interval} key={interval.id}>{
                                                interval.interval}</Dropdown.Item>) }
                                    </DropdownButton>
                                    { chosenInterval &&
                                        <p style={{marginLeft: "20px", fontSize: "1.1rem", color: "green"}}>
                                            You choose "{chosenInterval.title}"
                                        </p>
                                    }
                                </div>
                            </div> : ""
                        }
                        { chosenInterval && chosenCourse ?
                            <div className="flex-container-column" style={{marginTop: "20px"}}>
                                <div className="qr-input-header">Choose how much a QR should be available (sec): </div>
                                <input type="number"
                                    onChange = {(e) => hasChosenDuration(e.target.value)}
                                    className="form-control" 
                                    id="qrDuration" 
                                    aria-describedby="qrDuration" 
                                    placeholder="Enter in seconds how much should the QR be available"/>
                            </div> : ""
                        }
                        { chosenInterval && chosenCourse && chosenDuration ?
                            <div className="flex-container-column" style={{marginTop: "20px"}}>
                                <div className="qr-input-header">Choose how many times should the QR be regenerated: </div>
                                <input type="number"
                                    onChange = {(e) => hasChosenRepeats(e.target.value)}
                                    className="form-control" 
                                    id="qrDuration" 
                                    aria-describedby="qrDuration" 
                                    placeholder="Enter in seconds how much should the QR be available"/>
                            </div> : ""
                        }
                        { chosenInterval && chosenCourse && chosenDuration && chosenRepeats && !finishedSession ?
                            <div className="white-text-font create-qr-button-style create-qr-button-width"
                                onClick={submitQrSession}
                                style={{backgroundColor: theme.rose_budget}}>Start QR</div>
                                : ""
                        }
                        { chosenInterval && chosenCourse && chosenDuration && chosenRepeats && finishedSession ?
                            <div className="flex-container-row" style={{alignItems: "baseline"}}>
                                <div className="white-text-font create-qr-button-style create-qr-button-width"
                                        onClick={clearQrSession}
                                        style={{backgroundColor: theme.rose_budget}}>Reset</div>
                                <span style={{marginLeft: "20px", fontSize: "1.1rem"}}>Session is finished</span>
                            </div>
                                : ""
                        }

                    </div>
                </div>
                <div className="cornsilk-background 
                                qrsection-sections-width 
                                qrsection-section-page-height">'
                
                {receivedQrId && !finishedSession ? <QRCode value={receivedQrId + "/" + chosenCourse.id_curs + "/" + randomQrInt} /> : ""}
                {receivedQrId && !finishedSession ? <p>{receivedQrId + "/" + chosenCourse.id_curs + "/" + randomQrInt}</p> : ""}
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