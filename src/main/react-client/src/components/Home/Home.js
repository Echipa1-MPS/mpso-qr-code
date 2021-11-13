import { useEffect, useState, useContext } from "react";
import axios from 'axios';

export default function Home() {

    const mockCourses = ['EP', 'APP', 'MN'];

    const [courses, setCourses] = useState({});
    const [profile, setProfile] = useState({});

    return(
        <div className="flex-container-row-center full-height-vw">
            <div className="flex-container-row">
                <div className="aliceblue-background home-sections-width">
                    Teacher Details
                </div>
                <div className="cornsilk-background home-sections-width">
                    Course List
                </div>
            </div>
        </div>
    )
}