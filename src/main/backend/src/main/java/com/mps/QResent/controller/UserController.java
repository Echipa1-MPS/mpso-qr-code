package com.mps.QResent.controller;

import com.mps.QResent.model.User;
import com.mps.QResent.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import com.mps.QResent.security.Jwt;

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

    @PostMapping(path = "/register")
    public ResponseEntity<?> registerUser(@RequestBody User user) {
        try {
            if (((user.getEmail() != null && !Objects.equals(user.getEmail(), ""))
                    && (user.getPassword() != null && !Objects.equals(user.getPassword(), "")))) {
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

    @PostMapping(path = "/login")
    public ResponseEntity<?> loginUser(@RequestBody User user) {
        if (user.getEmail() == null || user.getPassword() == null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
        }
        try {
            authenticationManager.authenticate(new UsernamePasswordAuthenticationToken(user.getEmail(), user.getPassword()));
            String jwtToken = jwt.generateToken(user);
            return ResponseEntity.status(HttpStatus.OK).body(jwtToken);

        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

}
