package com.mps.QResent.service;

import com.mps.QResent.model.Subject;
import com.mps.QResent.repository.SubjectRepository;
import org.springframework.stereotype.Service;

import com.mps.QResent.projection.SubjectView;
import com.mps.QResent.repository.SubjectRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class SubjectService {
    private SubjectRepository subjectRepository;

    public SubjectService(SubjectRepository subjectRepository) {
        this.subjectRepository = subjectRepository;
    }

    public void save(Subject subject){
        subjectRepository.save(subject);
    }

    public void delete(Subject subject) {
        subjectRepository.delete(subject);
    }

    public List<SubjectView> getAll(){
        return subjectRepository.getAllBy();
    }

    public Optional<Subject> findById(Long id){
        return subjectRepository.findById(id);
    }
}
