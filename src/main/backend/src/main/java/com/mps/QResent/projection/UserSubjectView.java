package com.mps.QResent.projection;

import java.util.List;

public interface UserSubjectView {
    Long getId();
    String getFirstName();
    String getLastName();
    String getEmail();
    String getGroup();
    RoleView getRole();
    List<ScheduleSubjectView> getSubjectAfter();
}
