package com.mps.QResent.repository;

import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.projection.SubjectView;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;
import java.util.Set;

public interface SubjectRepository extends JpaRepository<Subject, Long> {
    List<SubjectView> getAllBy();
    Optional<Subject> findById(Long id);
}
