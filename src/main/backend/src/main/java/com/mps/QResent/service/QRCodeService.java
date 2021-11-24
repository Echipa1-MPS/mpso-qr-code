package com.mps.QResent.service;

import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.repository.QRCodeRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class QRCodeService {
    QRCodeRepository qrCodeRepository;

    public QRCodeService(QRCodeRepository qrCodeRepository) {
        this.qrCodeRepository = qrCodeRepository;
    }

    public List<QRCode> findAllBySchedule(Schedule schedule){
        return qrCodeRepository.findAllBySchedule(schedule);
    }
}
