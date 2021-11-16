package com.mps.QResent.model;

import javax.persistence.*;
import java.sql.Time;
import java.time.DayOfWeek;
import java.time.LocalTime;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "schedules")
public class Schedule {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "schedule_id")
    private Long id;

    @Column(name = "date")
    private DayOfWeek day;

    @ManyToOne(cascade = CascadeType.PERSIST)
    @JoinColumn(name = "subject_id")
    private Subject subject;

    @Column(name = "duration")
    private Integer length;

    @Column(name = "start_hour")
    private LocalTime startTime;

    @OneToMany(mappedBy = "schedule")
    private Set<QRCode> qrCodes = new HashSet<>();

    public Schedule() {
    }


    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public DayOfWeek getDay() {
        return day;
    }

    public void setDay(DayOfWeek day) {
        this.day = day;
    }

    public Integer getLength() {
        return length;
    }

    public void setLength(Integer length) {
        this.length = length;
    }

    public LocalTime getStartTime() {
        return startTime;
    }

    public void setStartTime(LocalTime startTime) {
        this.startTime = startTime;
    }

    public Subject getSubject() {
        return subject;
    }

    public void setSubject(Subject subject) {
        this.subject = subject;
    }

    public Set<QRCode> getQrCodes() {
        return qrCodes;
    }

    public void setQrCodes(Set<QRCode> qrCodes) {
        this.qrCodes = qrCodes;
    }
}
