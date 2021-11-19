package com.mps.QResent.service;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import com.mps.QResent.repository.UserRepository;
import net.minidev.json.JSONObject;
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

    public User findById(Long id) {
        Optional<User> user = this.userRepository.findById(id);
        return user.orElse(null);
    }


    public boolean isValidRole(Role role) {
        return role == Role.ADMIN || role == Role.TEACHER || role == Role.STUDENT;
    }

    public boolean isPresent(String email) {
        return userRepository.findByEmail(email).isPresent();
    }

    public boolean areValidCredentials(User user) {
        if (this.isValidRole(user.getRole())) {
            if (user.getRole() == Role.STUDENT) {
                return (!user.getName().isEmpty())
                        && (!user.getSurname().isEmpty())
                        && (!user.getGroup().isEmpty())
                        && (!user.getEmail().isEmpty())
                        && (!user.getPassword().isEmpty());
            } else if (user.getRole() == Role.TEACHER) {
                return (!user.getName().isEmpty())
                        && (!user.getSurname().isEmpty())
                        && (!user.getEmail().isEmpty())
                        && (!user.getPassword().isEmpty());
            } else return false;
        } else return false;
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