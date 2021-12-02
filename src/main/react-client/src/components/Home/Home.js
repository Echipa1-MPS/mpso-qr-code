import { useEffect, useState, useContext } from "react";
import stefan_p from '../../images/avatars/stefan_p.png';
import { getCoursesBrief, 
        getProfile,
        getUpcomingCourses } from "../../helpers/apicaller";
import { ThemeContext } from "../../App";

export default function Home() {

    const [courses, setCourses] = useState([]);
    const [profile, setProfile] = useState({});
    const [upcomingCourses, setUpcomingCourses] = useState([]);

    const theme = useContext(ThemeContext);

    useEffect(() => {
        const fetchCourses = async () => {
            getCoursesBrief(null, localStorage.getItem('user'), 
                successfulGetCoursesBrief, 
                failureGetCoursesBrief);
        }
        const fetchProfile = async () => {
            getProfile(null, localStorage.getItem('user'), 
                successfulGetProfile, 
                failureGetProfile);
        }
        const fetchUpcomingCourses = async () => {
            getUpcomingCourses(null, localStorage.getItem('user'), 
                successfulUpcomingCourses, 
                failureUpcomingCourses);
        }
        
        fetchCourses();
        fetchProfile();
        fetchUpcomingCourses();
    }, []);

    const navigateToCreateQr = () => window.location.href = "/create-qr";

    const successfulUpcomingCourses = (result) => setUpcomingCourses(result && result.data ? result.data : []);
    const failureUpcomingCourses = (error) => setUpcomingCourses([]);

    const successfulGetProfile = (result) => setProfile(result && result.data ? result.data : {});
    const failureGetProfile = (error) => setProfile({});

    const successfulGetCoursesBrief = (result) => setCourses(result && result.data ? JSON.parse(JSON.stringify(result.data)) : []);
    const failureGetCoursesBrief = (error) => setCourses([]);

    return(
        <div className="flex-container-row-center">
            <div className="flex-container-row home-sections-margin-top">
                <div className="light-blue-background home-sections-width home-section-page-height">
                    <div className="flex-container-row white-text-font" style={{alignItems: "center", justifyContent: "space-evenly", paddingTop: "20px"}}>
                        { profile ? (
                            <div className="flex-container-column">
                                <div style={{fontSize: '1.5rem'}}>Welcome Back,</div>
                                <div style={{fontSize: '1.3rem', fontWeight: "bold"}}>{profile.name} {profile.surname}</div>
                            </div>) : (<div style={{fontSize: "1.5rem"}}>No profile :(</div>)
                        }
                        <div>
                            <img src={stefan_p} alt="Stefan Popa" 
                                className="avatar rounded-avatar-image home-avatar-width avatar-margin avatar-border"/>
                        </div>
                    </div>

                    <div className="upcoming-courses-container">
                        <div className="white-text-font" style={{fontSize: "1.2rem", marginBottom: "20px"}}>
                            { upcomingCourses && upcomingCourses.length > 0 ? "Upcoming Courses" : "No upcoming courses" }
                        </div>
                        <div className="flex-container-row" style={{justifyContent: "space-between", flexWrap: "wrap"}}>
                            {
                                upcomingCourses && upcomingCourses.length > 0 ? 
                                    (upcomingCourses.map((course ) => {
                                    (
                                        <div className="upcoming-courses-item" key={course.interval}>
                                            <div style={{fontWeight: "bold"}}>{course.name}</div>
                                            <div>{course.interval}</div>
                                        </div>
                                    )})) 
                                    : ""
                            }
                        </div>
                    </div>

                    <div className="white-text-font create-qr-button-width create-qr-button-style" 
                        style={{ backgroundColor: theme.rose_budget}}
                        onClick = {navigateToCreateQr}>
                        Create QR
                    </div>
                </div>
                <div className="cornsilk-background home-sections-width">
                    <div style={{ paddingTop: "30px"}} >
                        {
                            courses && courses.length > 0 ?
                                <div className="flex-container-row home-courses-item-container" style={{alignItems: "center", justifyContent: "space-between", fontSize: "1.3rem"}}>
                                    <div style={{fontWeight: "bold"}}>Courses list</div>
                                    <div>{courses.length}</div>
                                </div> : ""
                        }

                        <div className="white-text-font">
                            { courses && courses.length > 0 && courses.map((course) => {
                                    return <div style={{fontSize: "1.2rem"}}className="home-courses-item-container light-blue-background" key={course.id}>{course.subject}</div>
                                })}
                            { (!courses || !courses.length) && (<div>No courses available</div>) }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

