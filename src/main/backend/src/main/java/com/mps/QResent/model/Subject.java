package com.mps.QResent.model;

import javax.persistence.*;
import java.util.Set;

@Entity
@Table(name = "subjects")
public class Subject {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "subject_id")
    private Long id;

    @Column(name = "name")
    private String name;

    @OneToMany(mappedBy = "subject")
    private Set<Schedule> schedule;

    @ManyToMany(mappedBy = "subjects")
    private Set<User> users;

    @Column(name = "information")
    private String infoSubject;

    public Subject() {
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Set<Schedule> getSchedule() {
        return schedule;
    }

    public void setSchedule(Set<Schedule> schedule) {
        this.schedule = schedule;
    }

    public Set<User> getUsers() {
        return users;
    }

    public void setUsers(Set<User> users) {
        this.users = users;
    }

    public String getInfoSubject() {
        return infoSubject;
    }

    public void setInfoSubject(String infoSubject) {
        this.infoSubject = infoSubject;
    }
}
