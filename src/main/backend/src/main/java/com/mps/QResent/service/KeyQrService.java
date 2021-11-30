package com.mps.QResent.service;

import com.mps.QResent.model.KeyQr;
import com.mps.QResent.repository.KeyQrRepository;
import org.springframework.stereotype.Service;

@Service
public class KeyQrService {

    private final KeyQrRepository keyQrRepository;

    public KeyQrService(KeyQrRepository keyQrRepository) {
        this.keyQrRepository = keyQrRepository;
    }

    public void save(KeyQr keyQr) {
        this.keyQrRepository.save(keyQr);
    }
}
