package com.mps.QResent.model;

import org.springframework.format.annotation.DateTimeFormat;

import javax.persistence.*;
import java.sql.Date;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "qr_code")
public class QRCode {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "qr_code_id")
    private Long id;

    @Column(name = "date")
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private Date date;

    @Column(name = "date_finish")
    @DateTimeFormat(pattern = "yyyy-MM-dd HH:mm:ss")
    private Date dateFinish;

    @ManyToOne(cascade = CascadeType.PERSIST)
    @JoinColumn(name = "schedule_id")
    private Schedule schedule;

    @ManyToMany(mappedBy = "qrCodes", cascade = CascadeType.PERSIST)
    private Set<User> users = new HashSet<>();

    public QRCode() {
    }



    public Schedule getSchedule() {
        return schedule;
    }

    public void setSchedule(Schedule schedule) {
        this.schedule = schedule;
    }

    public Set<User> getUsers() {
        return users;
    }

    public void setUsers(Set<User> users) {
        this.users = users;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public Schedule getSchedules() {
        return schedule;
    }

    public void setSchedules(Schedule schedules) {
        this.schedule = schedules;
    }

    public Date getDateFinish() {
        return dateFinish;
    }

    public void setDateFinish(Date dateFinish) {
        this.dateFinish = dateFinish;
    }
}
