export const getUpcomingCoursesMock = [
    {
        name: "MPS",
        interval: "marti 16.00-18.00"
    },
    {
        name: "EP",
        interval: "marti 18:00-20:00"
    },
    {
        name: "APP",
        interval: "miercuri 10:00-12:00"
    }
];

export const getCoursesBriefMock = [
    {
        id: 7,
        subject: "MPS"
    },
    {
        id: 8,
        subject: "EP"
    },
    {
        id: 9,
        subject: "APP"
    }
];

export const getCoursesDetailsMock = [
    {
        id: 7,
        title: "MPS",
        fullTitle: "Managementul Proiectelor Software",
        description: "Materie frumoasa de anul 4",
        intervals: [
            {
                id: 23,
                interval: "marti 16.00-18.00",
                series: "C5"
            },
            {
                id: 77,
                interval: "miercuri 10:00-12:00",
                series: "C4"
            }
        ]
    },
    {
        id: 8,
        title: "EP",
        fullTitle: "Evaluarea Performantelor",
        description: "Materie nemaipomenita de anul 4",
        intervals: [
            {
                id: 24,
                interval: "marti 18:00-20:00",
                series: "C5"
            },
            {
                id: 78,
                interval: "vineri 10:00-12:00",
                series: "C4"
            }
        ]
    }
];
