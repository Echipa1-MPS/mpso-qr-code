package com.mps.QResent.projection;

import java.sql.Time;
import java.time.LocalTime;

public interface ScheduleSubjectView {
    SubjectNameView getSubject();
    String getDay();
    Integer getLength();
    LocalTime getStartTime();
}
