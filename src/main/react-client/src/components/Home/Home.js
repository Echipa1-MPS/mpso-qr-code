import { useEffect, useState, useContext } from "react";
import stefan_p from '../../images/avatars/stefan_p.png';
import { getCourses, 
        getProfile,
        getUpcomingCourses } from "../../helpers/apicaller";
import { ThemeContext } from "../../App";

export default function Home() {

    const [courses, setCourses] = useState({});
    const [profile, setProfile] = useState({});
    const [upcomingCourses, setUpcomingCourses] = useState([]);

    const theme = useContext(ThemeContext);

    useEffect(() => {
        const fetchCourses = async () => {
            const result = await getCourses();
            setCourses(result);
        }
        fetchCourses();
    }, []);

    useEffect(() => {
        const fetchProfile = async () => {
            const result = await getProfile();
            setProfile(result);
        }
        console.log(theme);
        fetchProfile();
    }, []);

    useEffect(() => {
        const fetchPpcomingCourses = async () => {
            const result = await getUpcomingCourses();
            setUpcomingCourses(result);
        }
        fetchPpcomingCourses();
    }, []);

    return(
        <div className="flex-container-row-center home-section-page-height">
            <div className="flex-container-row home-sections-margin-top">
                <div className="light-blue-background home-sections-width">
                    <div className="flex-container-row white-text-font" style={{alignItems: "center", justifyContent: "space-evenly", paddingTop: "30px"}}>
                        { profile ? (
                            <div className="flex-container-column">
                                <div style={{fontSize: '1.5rem'}}>Welcome Back,</div>
                                <div style={{fontSize: '1.3rem'}}>{profile.name}</div>
                            </div>) : (<div style={{fontSize: "1.5rem"}}>No profile :(</div>)
                        }
                        <div>
                            <img src={stefan_p} alt="Stefan Popa" 
                                className="avatar rounded-avatar-image home-avatar-width avatar-margin avatar-border"/>
                        </div>
                    </div>

                    <div className="upcoming-courses-container">
                        <div className="white-text-font" style={{fontSize: "1.2rem", marginBottom: "20px"}}>Next courses list</div>
                        <div className="flex-container-row" style={{justifyContent: "space-between"}}>
                            {
                                upcomingCourses.length > 0 && upcomingCourses.map((course ) => {
                                    return (
                                        <div className="upcoming-courses-item" key={courses.name}>
                                            <div style={{fontWeight: "bold"}}>{course.name}</div>
                                            <div>{course.interval}</div>
                                        </div>
                                    )
                                })
                            }
                        </div>
                    </div>
                </div>
                <div className="cornsilk-background home-sections-width">
                    <div style={{ paddingTop: "30px"}} >
                        <div className="flex-container-row home-courses-item-container" style={{alignItems: "center", justifyContent: "space-between", fontSize: "1.3rem"}}>
                            <div style={{fontWeight: "bold"}}>Courses list</div>
                            <div>{courses.length}</div>
                        </div>
                        <div className="white-text-font">
                            { courses.length > 0 && courses.map((course) => {
                                    return <div style={{fontSize: "1.2rem"}}className="home-courses-item-container light-blue-background" key={course}>{course}</div>
                                })}
                            { (!courses || !courses.length) && (<div>No courses available</div>) }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

