package com.mps.QResent.projection;

import com.mps.QResent.enums.Role;

import java.util.List;

public interface UserSubjectView {
    Long getId();
    String getFirstName();
    String getLastName();
    String getEmail();
    String getGroup();
    Role getRole();
    List<SubjectView> getSubjects();
}
