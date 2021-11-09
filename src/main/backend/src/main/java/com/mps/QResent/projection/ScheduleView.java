package com.mps.QResent.projection;

import java.sql.Time;

public interface ScheduleView {
    String getDay();
    Time getLength();
    Time getStartTime();
}
