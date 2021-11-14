package com.mps.QResent.repository;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long>{
    public Optional<User> findByEmail(String email);
    public List<User> findByRole(Role role);
}
