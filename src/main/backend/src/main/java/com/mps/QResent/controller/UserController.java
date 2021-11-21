package com.mps.QResent.controller;

import com.mps.QResent.enums.Role;
import com.mps.QResent.model.User;
import com.mps.QResent.security.Jwt;
import com.mps.QResent.service.SubjectService;
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

import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;
import com.mps.QResent.projection.SubjectView;
import com.mps.QResent.projection.UserSubjectView;
import com.mps.QResent.service.ScheduleService;
import net.minidev.json.JSONArray;


import java.time.DayOfWeek;
import java.time.LocalDateTime;

import java.util.*;

@RestController
@RequestMapping(path = "/user")
public class UserController {
    @Autowired
    private UserService userService;

    @Autowired
    private SubjectService subjectService;
    @Autowired
    private PasswordEncoder passwordEncoder;

    @Autowired
    private Jwt jwt;

    @Autowired
    private AuthenticationManager authenticationManager;


    @Autowired
    private ScheduleService scheduleService;


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
            response.put("role", userService.findRoleByEmail(user.getEmail()).ordinal());
            response.put("jwt_token", jwtToken);
            return ResponseEntity.status(HttpStatus.OK).body(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }


    @PatchMapping(path = "/admin/update-user")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> updateUser(@RequestBody Map<String, Object> request) {
        try {
            if (request.get("user_id") != null) {
                User user = userService.findById(Long.parseLong(String.valueOf(request.get("user_id"))));
                for (Map.Entry<String, Object> entry : request.entrySet()) {
                    switch (entry.getKey()) {
                        case "user_id":
                            continue;
                        case "email":
                            user.setEmail((String) request.get("email"));
                            continue;
                        case "username":
                            user.setUsername((String) request.get("username"));
                            continue;
                        case "group":
                            user.setGroup((String) request.get("group"));
                            continue;
                        case "name":
                            user.setName((String) request.get("name"));
                            continue;
                        case "surname":
                            user.setSurname((String) request.get("surname"));
                            continue;
                        default:
                            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified key cannot be modified! You can only update the e-mail, username or group if you are a student.");
                    }
                }
                if (userService.isPresent(user.getEmail())) {
                    return ResponseEntity.status(HttpStatus.CONFLICT).body("This user already exists!");
                }
                userService.save(user);
                return ResponseEntity.status(HttpStatus.OK).body("The user has been successfully updated!");
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing the user ID!");
            }
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

    @DeleteMapping(path = "/admin/delete-user")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> deleteUser(@RequestBody User user) {
        try {
            JSONObject response = new JSONObject();
            response.put("user_id", userService.findUserIdByEmail(user.getEmail()));
            userService.deleteByEmail(user.getEmail());
            return ResponseEntity.status(HttpStatus.OK).body(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @PostMapping(path = "/admin/add-user")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> addUser(@RequestBody User user) {
        try {
            if (this.userService.areValidCredentials(user)) {
                if (!userService.isPresent(user.getEmail())) {
                    user.setPassword(passwordEncoder.encode(user.getPassword()));
                    this.userService.save(user);
                    JSONObject response = new JSONObject();
                    response.put("user_id", userService.findUserIdByEmail(user.getEmail()));
                    return ResponseEntity.status(HttpStatus.CREATED).body(response);
                } else {
                    return ResponseEntity.status(HttpStatus.CONFLICT).body("This user already exists!");
                }
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }
    @GetMapping(path = "/subjects")
    public String getSubjects() {
        UserSubjectView userSubjectView = userService.findUserNextCourses(userService.getCurrentUserEmail());
        JSONArray jsonArray = new JSONArray();
        for(SubjectView subjectView: userSubjectView.getSubjects()){
            System.out.println(DayOfWeek.from(LocalDateTime.now()));
            for(ScheduleSubjectView subjectView1: scheduleService.getNextSubjects(DayOfWeek.MONDAY, subjectView.getId())){
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("CourseName", subjectView1.getSubject().getName());
                jsonObject.put("day", subjectView1.getDay());
                jsonObject.put("length", subjectView1.getLength());
                jsonObject.put("startTime", subjectView1.getStartTime());
                jsonArray.add(jsonObject);
            }
        }
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("id_user", userSubjectView.getId());
        jsonObject.put("name", userSubjectView.getName());
        jsonObject.put("SecondName", userSubjectView.getSurname());
        jsonObject.put("LDAP", userSubjectView.getEmail());
        jsonObject.put("Group", userSubjectView.getGroup());
        jsonObject.put("privilege", userSubjectView.getRole());
        jsonObject.put("NextCourses", jsonArray);
        return jsonObject.toString();
    }

    @GetMapping(path = "/allCourses")
    public String getAllCourses(){
        Optional<User> user = userService.findByEmail(userService.getCurrentUserEmail());
        JSONObject jsonObject = new JSONObject();
        JSONArray courses_enrolled = new JSONArray();
        if(user.isPresent()){
            for(Subject subject: user.get().getSubjects()){
                JSONObject course = new JSONObject();
                course.put("id_course", subject.getId());
                course.put("name_course", subject.getName());
                course.put("desc", subject.getInfoSubject());
                course.put("grading", subject.getGradingSubject());
                course.put("name_prof", userService.getProf(subject));
                JSONArray intervals = new JSONArray();
                for(Schedule schedule: subject.getSchedule()){
                    JSONObject interval = new JSONObject();
                    interval.put("id_interval", schedule.getId());
                    interval.put("day", schedule.getDay());
                    interval.put("length", schedule.getLength());
                    interval.put("start_h", schedule.getStartTime());
                    intervals.add(interval);
                }
                course.put("Intervals", intervals);
                JSONArray students = new JSONArray();
                for(User student: userService.getStudents(subject)){
                    students.add(studentJSON(student));
                }
                course.put("Students_Enrolled", students);
                courses_enrolled.add(course);
            }
        }
        jsonObject.put("count", courses_enrolled.size());
        jsonObject.put("courses_enrolled", courses_enrolled);
        return jsonObject.toString();
    }

    static JSONObject studentJSON(User student) {
        JSONObject studentJson = new JSONObject();
        studentJson.put("name", student.getName());
        studentJson.put("secondName", student.getSurname());
        studentJson.put("ldap", student.getEmail());
        studentJson.put("group", student.getGroup());
        studentJson.put("privilege", student.getRole());
        return studentJson;
    }
}
