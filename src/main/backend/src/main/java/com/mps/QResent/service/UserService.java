package com.mps.QResent.service;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import com.mps.QResent.repository.UserRepository;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.UserSubjectView;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
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

    public Role findRoleByEmail(String email) {
        Optional<User> user = this.userRepository.findByEmail(email);
        return user.map(User::getRole).orElse(null);
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

    public Optional<User> findByIdOptional(Long id){
        return userRepository.findById(id);
    }

    public String getProf(Subject subject){
        String profName = "";
        for(User user: userRepository.findAllByRole(Role.TEACHER)){
            if(user.getSubjects().contains(subject)){
                return profName + user.getName() + user.getSurname();
            }
        }
        return profName;
    }

    public Long getProfId(Subject subject){
        for(User user: userRepository.findAllByRole(Role.TEACHER)){
            if(user.getSubjects().contains(subject)){
                return user.getId();
            }
        }
        return 0L;
    }

    public List<User> getStudents(Subject subject){
        return userRepository.findAllByRole(Role.STUDENT);
    }

}
