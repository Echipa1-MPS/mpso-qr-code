package com.mps.QResent.controller;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import com.mps.QResent.security.Jwt;
import com.mps.QResent.service.UserService;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

@RestController
@RequestMapping(path = "/user")
public class UserController {
    @Autowired
    private UserService userService;
    @Autowired
    private PasswordEncoder passwordEncoder;
    @Autowired
    private Jwt jwt;

    @Autowired
    private AuthenticationManager authenticationManager;

    @PostMapping(path = "/authentication/register")
    public ResponseEntity<?> registerUser(@RequestBody User user) {
        try {
            if (((user.getEmail() != null && !Objects.equals(user.getEmail(), ""))
                    && (user.getPassword() != null && !Objects.equals(user.getPassword(), ""))
                    && (userService.isValidRole(user.getRole())))) {
                if (!userService.isPresent(user.getEmail())) {
                    user.setPassword(passwordEncoder.encode(user.getPassword()));
                    userService.save(user);
                    return ResponseEntity.status(HttpStatus.CREATED).body("You have been successfully registered!");
                } else {
                    return ResponseEntity.status(HttpStatus.CONFLICT).body("This e-mail already exists!");
                }
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @PostMapping(path = "/authentication/login")
    public ResponseEntity<?> loginUser(@RequestBody User user) {
        if (user.getEmail() == null || user.getPassword() == null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
        }
        try {
            authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(user.getEmail(), user.getPassword()));
            String jwtToken = jwt.generateToken(user);
            JSONObject response = new JSONObject();
            response.put("user_id", userService.findUserIdByEmail(user.getEmail()));
            response.put("jwt_token", jwtToken);
            return ResponseEntity.status(HttpStatus.OK).body(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @GetMapping(path = "/admin/get-students")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> getStudents() {
        try {
            List<User> users = this.userService.findUsersByRole(Role.STUDENT);
            JSONObject response = new JSONObject();
            List<JSONObject> students = new ArrayList<>();
            for (User user : users) {
                JSONObject student = new JSONObject();
                student.put("user_id", user.getId());
                student.put("surname", user.getSurname());
                student.put("name", user.getName());
                student.put("username", user.getUsername());
                student.put("email", user.getEmail());
                student.put("group", user.getGroup());
                students.add(student);
            }
            response.put("students", students);
            return ResponseEntity.status(HttpStatus.OK).body(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @GetMapping(path = "/admin/get-teachers")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> getTeachers() {
        try {
            List<User> users = this.userService.findUsersByRole(Role.TEACHER);
            JSONObject response = new JSONObject();
            List<JSONObject> teachers = new ArrayList<>();
            for (User user : users) {
                JSONObject teacher = new JSONObject();
                teacher.put("user_id", user.getId());
                teacher.put("surname", user.getSurname());
                teacher.put("name", user.getName());
                teacher.put("username", user.getUsername());
                teacher.put("email", user.getEmail());
                teachers.add(teacher);
            }
            response.put("teachers", teachers);
            return ResponseEntity.status(HttpStatus.OK).body(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }
}
