package com.mps.QResent.repository;

import com.mps.QResent.model.Subject;
import org.springframework.data.jpa.repository.JpaRepository;
import com.mps.QResent.projection.SubjectView;

import java.util.List;
import java.util.Optional;
import java.util.Set;

public interface SubjectRepository extends JpaRepository<Subject, Long> {
    List<SubjectView> getAllBy();
    Optional<Subject> findById(Long id);
    List<Subject> findAll();
    void deleteById(Long id);
}
