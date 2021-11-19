import {
    getCoursesBriefMock,
    getCoursesDetailsMock,
    getUpcomingCoursesMock,
}
from './mockup';

export const apiHostUrl = '';
export const postCreateQrUrl = `${apiHostUrl}/api/create-qr`;
export const postUpdateQrUrl = `${apiHostUrl}/api/update-qr`;
export const getHomeCoursesUrl = `${apiHostUrl}/api/courses`;
export const getHomeProfileUrl = `${apiHostUrl}/api/profile`;
export const getCoursesBriefUrl = `${apiHostUrl}/api/courses/brief`;
export const getCoursesDetailsUrl = `${apiHostUrl}/api/course/details`;
export const getUpcomingCoursesUrl = `${apiHostUrl}/api/upcoming/courses`;

export function getCoursesBrief(params, success, failure) {
    return getCoursesBriefMock;
}

export function getCoursesDetails(params, success, failure) {
    return getCoursesDetailsMock;
}

export function getProfile(params, success, failure) {
    return { name: 'Stefan Popa', email: 'stefan.popa@gmail.com' };
}

export function getUpcomingCourses(params, success, failure) {
    return getUpcomingCoursesMock;
}

