package com.mps.QResent.runnables;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.service.ScheduleService;
import com.mps.QResent.service.SubjectService;
import com.mps.QResent.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

import java.time.DayOfWeek;
import java.time.LocalTime;
import java.util.*;

@Component
public class Populator implements CommandLineRunner {
    @Autowired
    ScheduleService scheduleService;

    @Autowired
    SubjectService subjectService;

    @Autowired
    UserService userService;

    @Autowired
    PasswordEncoder passwordEncoder;

    @Override
    public void run(String... args) throws Exception {
        ArrayList<User> users = new ArrayList<>();
        List<String> emails = Arrays.asList("emailtest@gmail.com", "emailtest2@gmail.com");
        List<String> firstNames = Arrays.asList("Andrei", "Bogdan");
        List<String> lastNames = Arrays.asList("Ion", "Buliga");
        List<String> passwords = Arrays.asList("12345678", "password");


        Subject MPS, PS, SMP;
        Schedule luni, marti, miercuri;
        luni = new Schedule();
        luni.setDay(DayOfWeek.MONDAY);
        luni.setLength(2);
        luni.setStartTime(LocalTime.of(10,0,0));
        scheduleService.save(luni);

        marti = new Schedule();
        marti.setDay(DayOfWeek.THURSDAY);
        marti.setLength(2);
        marti.setStartTime(LocalTime.of(14,0,0));
        scheduleService.save(marti);

        miercuri = new Schedule();
        miercuri.setDay(DayOfWeek.WEDNESDAY);
        miercuri.setLength(2);
        miercuri.setStartTime(LocalTime.of(16,0,0));
        scheduleService.save(miercuri);

        MPS = new Subject();
        MPS.setGradingSubject("Nota 10 pentru orice");
        MPS.setName("MPS");
        MPS.setInfoSubject("It's for people who wants to be a CEO");
        subjectService.save(MPS);
        MPS.getSchedule().add(marti);
        marti.setSubject(MPS);
        scheduleService.save(marti);
//        subjectService.save(MPS);
//        for (String email : emails) {
//            Optional<User> user = userService.findByEmail(email);
//            System.out.println(user.isPresent());
//            user.ifPresent(value -> MPS.getUsers().add(value));
//        }
        subjectService.save(MPS);
//        System.out.println(MPS.getUsers());
//        for (String email : emails) {
//            Optional<User> user = userService.findByEmail(email);
//            user.ifPresent(value -> System.out.println(value.getId()));
//            System.out.println(MPS.getId());
//            user.ifPresent(value -> value.getSubjects().add(MPS));
//            user.ifPresent(value -> userService.save(value));
//        }

        PS = new Subject();
        PS.setGradingSubject("Nota 10 pentru orice");
        PS.setName("PS");
        PS.setInfoSubject("It's for people who wants to be a CEO");
//        PS.setUsers(users_set);
        subjectService.save(PS);
        PS.getSchedule().add(marti);

        subjectService.save(PS);

        for (int i = 0; i < emails.size(); i++) {
            if (userService.findByEmail(emails.get(i)).isEmpty()) {
                User user = new User();
                user.setEmail(emails.get(i));
                user.setName(firstNames.get(i));
                user.setSurname(lastNames.get(i));
                user.setPassword(passwordEncoder.encode(passwords.get(i)));
                user.setRole(Role.STUDENT);
                userService.save(user);
                user.getSubjects().add(MPS);
                userService.save(user);
                users.add(user);
            }
        }
        User admin;
        if (userService.findByEmail("admin@gmail.com").isEmpty()) {
            admin = new User();
            admin.setEmail("admin@gmail.com");
            admin.setName("Admin");
            admin.setSurname("Admin");
            admin.setRole(Role.TEACHER);
            admin.setPassword(passwordEncoder.encode("admin"));
            userService.save(admin);
            admin.getSubjects().add(MPS);
            userService.save(admin);
        }

    }
}
