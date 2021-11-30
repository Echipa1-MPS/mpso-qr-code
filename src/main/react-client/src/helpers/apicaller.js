import {
    getCoursesBriefMock,
    getCoursesDetailsMock,
    getUpcomingCoursesMock,
}
from './mockup';

const axios = require('axios');

export const apiHostUrl = 'http://3.18.103.144:8080';
//DONE (POST)
export const postLoginUrl = `${apiHostUrl}/api/user/authentication/login`;
//IN PROGRESS (POST)
export const postCreateQrUrl = `${apiHostUrl}/api/qr/teacher/generate-qr-id`;
//IN PROGRESS (PATCH)
export const patchUpdateQrUrl = `${apiHostUrl}/api/qr/teacher/update-qr-key`;

export const getHomeCoursesUrl = `${apiHostUrl}/api/courses`; //pune teacherId in Body
export const getHomeProfileUrl = `${apiHostUrl}/api/profile`; //pune teacherId in Body
export const getCoursesBriefUrl = `${apiHostUrl}/api/courses/brief`; //pune teacherId in Body, returnezi un array de strings ['EP', 'APP', 'MP']
export const getCoursesDetailsUrl = `${apiHostUrl}/api/course/details`; //pune teacherId in Body, imi returnezi cursurile cu intervale cu tot
export const getUpcomingCoursesUrl = `${apiHostUrl}/api/upcoming/courses/:coursesCount`; // imi retunrezi 
 
export function postLogin(params, success, failure) {
    axios({
        method: 'post',
        url: postLoginUrl,
        data: params, //email | password
        headers: {
          'content-type': 'application/json; charset=utf-8'
        }
  })
  .then((response) => {
      success(response);
  }, (error) => {
      failure(error);
  });
}

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

export function postCreateQr(params, jwt, success, failure) {
    axios({
        method: 'post',
        url: postCreateQrUrl,
        data: params, //schedule | reps | offset | subject | key
        headers: {
            Authorization:`Bearer ${jwt}`,
            'content-type': 'application/json; charset=utf-8'
        }
  })
  .then((response) => {
      success(response);
  }, (error) => {
      failure(error);
  });
}

export function patchUpdateQr(params, jwt, success, failure) {
    axios({
        method: 'patch',
        url: patchUpdateQrUrl,
        data: params, //qr_id | key
        headers: {
            Authorization:`Bearer ${jwt}`,
            'content-type': 'application/json; charset=utf-8'
        }
  })
  .then((response) => {
      success(response);
  }, (error) => {
      failure(error);
  });
}