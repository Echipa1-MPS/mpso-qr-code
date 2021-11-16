package com.mps.QResent.repository;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.projection.UserSubjectView;
import com.mps.QResent.service.UserService;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Long>{
    Optional<User> findByEmail(String email);
    UserSubjectView findAllByEmail(String email);
    Optional<User> findById(Long id);
    List<User> findAllByRole(Role role);
}
