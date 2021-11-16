package com.mps.QResent.service;

import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.projection.UserSubjectView;
import com.mps.QResent.repository.UserRepository;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

import java.util.List;
import java.util.Optional;

@Service
public class UserService implements UserDetailsService {
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

    public boolean isPresent(String email) {
        return userRepository.findByEmail(email).isPresent();
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
                .roles("USER")
                .build();
    }

    public UserSubjectView findUserNextCourses(String name){
//        return userRepository.findAllByEmail(getCurrentUserEmail());
        System.out.println(name);
        return userRepository.findAllByEmail(name);
    }

    public String getCurrentUserEmail() {
        Authentication auth = SecurityContextHolder.getContext().getAuthentication();
        return auth.getPrincipal().toString();
    }

    public Optional<User> findByEmail(String email){
        System.out.println("in find by email");
        return userRepository.findByEmail(email);
    }

    public Optional<User> findById(Long id){
        return userRepository.findById(id);
    }

    public String getProf(Subject subject){
        String profName = "";
        for(User user: userRepository.findAllByRole(1)){
            if(user.getSubjects().contains(subject)){
                return profName + user.getName() + user.getSurname();
            }
        }
        return profName;
    }

    public List<User> getStudents(Subject subject){
        return userRepository.findAllByRole(0);
    }
}
