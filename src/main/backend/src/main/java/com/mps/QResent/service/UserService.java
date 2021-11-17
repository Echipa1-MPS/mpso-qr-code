package com.mps.QResent.service;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import com.mps.QResent.repository.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class UserService implements UserDetailsService {
    private final UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public Long findUserIdByEmail(String email) {
        Optional<User> user = this.userRepository.findByEmail(email);
        return user.map(User::getId).orElse(null);
    }

    public List<User> findUsersByRole(Role role) {
        return this.userRepository.findByRole(role);
    }

    public boolean isValidRole(Role role) {
        return role == Role.ADMIN || role == Role.TEACHER || role == Role.STUDENT;
    }

    public boolean isPresent(String email) {
        return userRepository.findByEmail(email).isPresent();
    }

    public void save(User user) {
        userRepository.save(user);
    }

    public void deleteByEmail(String email) {
        if (this.userRepository.findByEmail(email).isPresent()) {
            userRepository.delete(this.userRepository.findByEmail(email).get());
        }
    }

    @Override
    public UserDetails loadUserByUsername(String s) throws UsernameNotFoundException {
        Optional<User> optional = userRepository.findByEmail(s);
        if (optional.isEmpty()) {
            throw new UsernameNotFoundException(s);
        }

        User user = optional.get();
        return org.springframework.security.core.userdetails.User
                .withUsername(user.getEmail())
                .password(user.getPassword())
                .roles(String.valueOf(user.getRole()))
                .build();
    }
}