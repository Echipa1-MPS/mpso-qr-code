package com.mps.QResent.repository;

import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface QRCodeRepository extends JpaRepository<QRCode,Long> {
    List<QRCode> findAllBySchedule(Schedule schedule);
}
