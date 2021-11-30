package com.mps.QResent.projection;

import java.time.LocalTime;

public interface ScheduleSubjectView {
    SubjectNameView getSubject();
    String getDay();
    Integer getLength();
    LocalTime getStartTime();
}
