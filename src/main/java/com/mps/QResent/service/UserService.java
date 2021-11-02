package com.mps.QResent.service;

import com.mps.QResent.model.User;
import com.mps.QResent.repository.UserRepository;
import org.springframework.stereotype.Service;

@Service
public class UserService {
    private final UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public void save(User user){
        userRepository.save(user);
    }

    public void delete(User user){
        userRepository.delete(user);
    }
}
