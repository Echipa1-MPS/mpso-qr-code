package com.mps.QResent.dto;

import java.util.List;

public class StudentsEnroll {
    private Long id_course;
    private List<StudentsToEnroll> students_to_enroll;

    public Long getId_course() {
        return id_course;
    }

    public void setId_course(Long id_course) {
        this.id_course = id_course;
    }

    public List<StudentsToEnroll> getStudents_to_enroll() {
        return students_to_enroll;
    }

    public void setStudents_to_enroll(List<StudentsToEnroll> students_to_enroll) {
        this.students_to_enroll = students_to_enroll;
    }

}
