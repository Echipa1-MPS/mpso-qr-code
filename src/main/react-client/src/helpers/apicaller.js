import {
    getCoursesBriefMock,
    getCoursesDetailsMock,
    getUpcomingCoursesMock,
}
from './mockup';

const axios = require('axios');

export const apiHostUrl = 'http://3.18.103.144:8080';
// DONE (POST)
export const postLoginUrl = `${apiHostUrl}/api/user/authentication/login`;
// DONE (POST)
export const postCreateQrUrl = `${apiHostUrl}/api/qr/teacher/generate-qr-id`;
// DONE (PATCH)
export const patchUpdateQrUrl = `${apiHostUrl}/api/qr/teacher/update-qr-key`;
// DONE (GET)
export const getProfileUrl = `${apiHostUrl}/api/user/teacher/profile`;
// SERVER ERROR
export const getUpcomingCoursesUrl = `${apiHostUrl}/api/upcoming/courses`;
// DONE (GET)
export const getCoursesBriefUrl = `${apiHostUrl}/api/subject/teacher/courses/brief`;
// DONE (GET)
export const getCoursesDetailsUrl = `${apiHostUrl}/api/subject/teacher/courses/details`; //pune teacherId in Body, imi returnezi cursurile cu intervale cu tot
 
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

export function getCoursesBrief(_, jwt, success, failure) {
    axios({
        method: 'get',
        url: getCoursesBriefUrl,
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

export function getCoursesDetails(_, jwt, success, failure) {
    axios({
        method: 'get',
        url: getCoursesDetailsUrl,
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

export function getProfile(params, jwt, success, failure) {
    axios({
        method: 'get',
        url: getProfileUrl,
        data: params,
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

export function getUpcomingCourses(_, jwt, success, failure) {
    axios({
        method: 'get',
        url: getUpcomingCoursesUrl,
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