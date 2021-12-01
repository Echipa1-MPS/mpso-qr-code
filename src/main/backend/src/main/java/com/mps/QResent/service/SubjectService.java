package com.mps.QResent.service;

import com.mps.QResent.model.Subject;
import com.mps.QResent.repository.SubjectRepository;
import org.springframework.stereotype.Service;

import com.mps.QResent.projection.SubjectView;

import java.util.List;
import java.util.Optional;

@Service
public class SubjectService {
    private final SubjectRepository subjectRepository;

    public SubjectService(SubjectRepository subjectRepository) {
        this.subjectRepository = subjectRepository;
    }

    public void save(Subject subject){
        subjectRepository.save(subject);
    }

    public void delete(Long id) {
        subjectRepository.deleteById(id);
    }

    public List<SubjectView> getAll(){
        return subjectRepository.getAllBy();
    }

    public Optional<Subject> findById(Long id){
        return subjectRepository.findById(id);
    }

    public List<Subject> getAllModelView(){
        return subjectRepository.findAll();
    }
}
