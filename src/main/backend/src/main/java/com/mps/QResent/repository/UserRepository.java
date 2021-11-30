package com.mps.QResent.repository;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import com.mps.QResent.projection.UserSubjectView;

import java.util.List;
import java.util.Optional;


@Repository
public interface UserRepository extends JpaRepository<User, Long>{
    List<User> findByRole(Role role);
    Optional<User> findByEmail(String email);
    UserSubjectView findAllByEmail(String email);
    Optional<User> findById(Long id);
    List<User> findAllByRole(Role role);
}
