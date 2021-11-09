package com.mps.QResent.projection;

import java.sql.Time;

public interface ScheduleSubjectView {
    SubjectNameView getSubject();
    String getDay();
    Time getLength();
    Time getStartTime();
}
