package com.mps.QResent.repository;

import com.mps.QResent.model.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Long>{
}
