export const apiHostUrl = '';
export const postCreateQrUrl = `${apiHostUrl}/api/create-qr`;
export const postUpdateQrUrl = `${apiHostUrl}/api/update-qr`;
export const getHomeCoursesUrl = `${apiHostUrl}/api/courses`;
export const getHomeProfileUrl = `${apiHostUrl}/api/profile`;
export const getCoursesBriefUrl = `${apiHostUrl}/api/courses/brief`;
export const getCourseDetailsUrl = `${apiHostUrl}/api/course/details`;
export const getUpcomingCoursesUrl = `${apiHostUrl}/api/upcoming/courses`;

export function getCourses(params, success, failure) {

    return ['EP', 'APP', 'SPRC'];
    //return [];
    // axios.get(getHomeCoursesUrl, { params })
    //     .then(success)
    //     .catch(failure);
}

export function getProfile(params, success, failure) {
    return { name: 'Stefan Popa', email: 'stefan.popa@gmail.com' };
    // axios.get(getHomeProfileUrl, { params })
    //     .then(success)
    //     .catch(failure);
}

export function getUpcomingCourses(params, success, failure) {
    return [
        { name: "MPS", interval: "marti 16.00-18.00"},
        { name: "EP", interval: "marti 18:00-20:00"},
        { name: "APP", interval: "miercuri 10:00-12:00"},
    ];
    // axios.get(getUpcomingCoursesUrl, { params })
    //     .then(success)
    //     .catch(failure);
}

