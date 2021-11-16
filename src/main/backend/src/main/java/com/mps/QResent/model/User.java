package com.mps.QResent.model;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "users")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_id")
    private Long id;

    @Column(name = "role")
    private Integer role;

    @Column(name = "email")
    private String email;

    @Column(name = "password")
    private String password;

    @Column(name = "name")
    private String name;

    @Column(name = "surname")
    private String surname;

    @Column(name = "group_label")
    private String group;

    @ManyToMany(cascade = CascadeType.PERSIST)
    @JoinTable(
            name = "user_subject",
            joinColumns = {@JoinColumn(name = "user_id")},
            inverseJoinColumns = {@JoinColumn(name = "subject_id")}
    )
    private Set<Subject> subjects = new HashSet<>();;

    @ManyToMany(cascade = CascadeType.PERSIST)
    @JoinTable(
            name = "presence",
            joinColumns = {@JoinColumn(name = "user_id")},
            inverseJoinColumns = {@JoinColumn(name = "qr_code_id")}
    )
    private Set<QRCode> qrCodes = new HashSet<>();;

    public User() {
    }


    public Set<QRCode> getQrCodes() {
        return qrCodes;
    }

    public void setQrCodes(Set<QRCode> qrCodes) {
        this.qrCodes = qrCodes;
    }

    public String getName() {
        return name;
    }

    public void setName(String firstName) {
        this.name = firstName;
    }

    public String getSurname() {
        return surname;
    }

    public void setSurname(String lastName) {
        this.surname = lastName;
    }

    public Set<Subject> getSubjects() {
        return subjects;
    }

    public void setSubjects(Set<Subject> subjects) {
        this.subjects = subjects;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Integer getRole() {
        return role;
    }

    public void setRole(Integer role) {
        this.role = role;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getGroup() {
        return group;
    }

    public void setGroup(String group) {
        this.group = group;
    }
}
