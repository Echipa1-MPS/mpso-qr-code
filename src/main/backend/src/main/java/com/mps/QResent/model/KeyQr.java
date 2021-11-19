package com.mps.QResent.model;

import javax.persistence.*;

@Entity
@Table(name = "keysQr")
public class KeyQr {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "key_qr_id")
    private Long id;

    @Column(name = "key_qr_value")
    private String keyValue;

    @OneToOne(mappedBy = "keyQr", cascade = CascadeType.PERSIST)
    private Subject subject;

    public KeyQr() {
    }



    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getKeyValue() {
        return keyValue;
    }

    public void setKeyValue(String keyValue) {
        this.keyValue = keyValue;
    }

    public Subject getSubject() {
        return subject;
    }

    public void setSubject(Subject subject) {
        this.subject = subject;
    }
}
