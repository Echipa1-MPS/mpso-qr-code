package com.mps.QResent.projection;

import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.User;

import java.util.List;

public interface SubjectView {
    Long getId();
    String getName();
//    List<User> getUsers();
    List<ScheduleSubjectView> getSchedule();
//    List<UserSubjectView> getUsers();
    String getInfoSubject();
    String getGradingSubject();
}
