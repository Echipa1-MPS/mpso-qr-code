package com.mps.QResent.model;

import javax.persistence.*;
import java.util.HashSet;
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

    @OneToMany(mappedBy = "subject", cascade = CascadeType.ALL, orphanRemoval = true)
    private Set<Schedule> schedule = new HashSet<>();

    @ManyToMany(mappedBy = "subjects", cascade = CascadeType.ALL)
    private Set<User> users = new HashSet<>();


    @Column(name = "information")
    private String infoSubject;

    @Column(name = "grading")
    private String gradingSubject;

    @OneToOne(cascade = CascadeType.ALL, orphanRemoval = true)
    @JoinColumn(name = "key_qr_id")
    private KeyQr keyQr;


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

    public String getGradingSubject() {
        return gradingSubject;
    }

    public void setGradingSubject(String gradingSubject) {
        this.gradingSubject = gradingSubject;
    }

    public KeyQr getKeyQr() {
        return keyQr;
    }

    public void setKeyQr(KeyQr keyQr) {
        this.keyQr = keyQr;
    }

}
