package com.mps.QResent.controller;

import com.mps.QResent.dto.StudentsEnroll;
import com.mps.QResent.dto.StudentsToEnroll;
import com.mps.QResent.dto.SubjectDTO;
import com.mps.QResent.enums.Role;
import com.mps.QResent.helper.Helper;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.projection.ScheduleSubjectView;
import com.mps.QResent.projection.SubjectView;
import com.mps.QResent.projection.UserSubjectView;
import com.mps.QResent.service.ScheduleService;
import com.mps.QResent.service.SubjectService;
import com.mps.QResent.service.UserService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;

import java.time.DayOfWeek;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;

@CrossOrigin(maxAge = 3600)
@RestController
@RequestMapping(path = "/subject")
public class SubjectController {
    @Autowired
    SubjectService subjectService;

    @Autowired
    UserService userService;

    @Autowired
    private ScheduleService scheduleService;

    @GetMapping(path = "/admin/get-all-subjects")
    @RolesAllowed("ADMIN")
    public List<SubjectView> getAll() {
        return subjectService.getAll();
    }

    @GetMapping(path = "/admin/get-all-courses")
    @RolesAllowed("ADMIN")
    public String getAllCourses() {
        List<SubjectView> subjects = subjectService.getAll();
        JSONObject jsonObject = new JSONObject();
        JSONArray jsonArray = new JSONArray();
        for (SubjectView subject : subjects) {
            JSONObject jsonObject1 = new JSONObject();
            jsonObject1.put("Id_Course", subject.getId());
            jsonObject1.put("Name_C", subject.getName());
            subjectService.findById(subject.getId()).ifPresent(value -> jsonObject1.put("Id_Professor", userService.getProfId(value)));
            subjectService.findById(subject.getId()).ifPresent(value -> jsonObject1.put("Professor_Name", userService.getProf(value)));
            jsonObject1.put("Desc", subject.getInfoSubject());
            jsonObject1.put("Grading", subject.getGradingSubject());
            JSONArray jsonArray1 = new JSONArray();
            for(ScheduleSubjectView scheduleSubjectView: subject.getSchedule()){
                JSONObject jsonObject2 = new JSONObject();
                jsonObject2.put("day", scheduleSubjectView.getDay());
                jsonObject2.put("start_h", scheduleSubjectView.getStartTime().getHour());
                jsonObject2.put("length", scheduleSubjectView.getLength());
                jsonArray1.add(jsonObject2);
            }
            JSONArray students = new JSONArray();
            if(subjectService.findById(subject.getId()).isPresent()) {
                students = userService.getStudents(subjectService.findById(subject.getId()).get()).stream().map(Helper::studentJSON).collect(Collectors.toCollection(JSONArray::new));
            }
            jsonObject1.put("Students_Enrolled", students);
            jsonObject1.put("intervals", jsonArray1);
            jsonArray.add(jsonObject1);
        }
        jsonObject.put("Courses", jsonArray);
        return jsonObject.toString();
    }

    @DeleteMapping(path = "/admin/delete-course/{id}")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> deleteSubject(@PathVariable Long id) {
        try {
            subjectService.delete(id);
            return ResponseEntity.status(HttpStatus.OK).body("Deleted successfully");
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @PostMapping(path = "/admin/create-course")
    @RolesAllowed("ADMIN")
    public Long createSubject(@RequestBody SubjectDTO subjectDTO) {
        Subject subject = new Subject();
        subject.setInfoSubject(subjectDTO.getDesc());
        subject.setName(subjectDTO.getNameC());
        subject.setGradingSubject(subjectDTO.getGrading());
        subjectService.save(subject);
        if (userService.findByIdOptional(subjectDTO.getIdProfessor()).isPresent()) {
            subject.getUsers().add(userService.findByIdOptional(subjectDTO.getIdProfessor()).get());
            subjectService.save(subject);
            userService.findByIdOptional(subjectDTO.getIdProfessor()).get().getSubjects().add(subject);
            userService.save(userService.findByIdOptional(subjectDTO.getIdProfessor()).get());
        }
        return subject.getId();
    }

    @PostMapping(path = "/update-course")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed({"ADMIN", "TEACHER"})
    public ResponseEntity<?> updateSubject(@RequestBody Map<String, Object> request) {
        try {
            if (request.get("course_id") != null) {
                Long course_id = Long.valueOf((Integer) request.get("course_id"));
                Optional<Subject> subject = subjectService.findById(course_id);
                Optional<User> userRequest = userService.findByEmail(userService.getCurrentUserEmail());
                assert userRequest.isPresent();
                assert subject.isPresent();
                for (Map.Entry<String, Object> entry : request.entrySet()) {
                    switch (entry.getKey()) {
                        case "course_id":
                            continue;
                        case "nameC":
                            if(userRequest.get().getRole() == Role.ADMIN) {
                                subject.ifPresent(value -> value.setName((String) request.get("nameC")));
                                continue;
                            }
                            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified key cannot be modified, not an admin");
                        case "idProfessor":
                            if(userRequest.get().getRole() == Role.ADMIN) {
                                if(userService.getProfId(subject.get()) != null) {
                                    userService.findById(userService.getProfId(subject.get())).getSubjects().remove(subject.get());
                                    subject.get().getUsers().remove(userService.findById(userService.getProfId(subject.get())));
                                }
                                Optional<User> user = userService.findByIdOptional(Long.valueOf((Integer) request.get("idProfessor")));
                                user.ifPresent(value -> subject.ifPresent(value1 -> value1.getUsers().add(value)));
                                user.ifPresent(value -> userService.save(value));
                                continue;
                            }
                            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified key cannot be modified, not an admin");

                        case "desc":
                            subject.ifPresent(value -> value.setInfoSubject((String) request.get("desc")));
                            continue;
                        case "grading":
                            subject.ifPresent(value -> value.setGradingSubject((String) request.get("grading")));
                            continue;
                        default:
                            return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified key cannot be modified.");
                    }
                }
                subject.ifPresent(value -> subjectService.save(value));
                return ResponseEntity.status(HttpStatus.OK).body("The course has been successfully updated!");
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing the course ID!");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @PostMapping(path = "/admin/enroll-students")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> enrollStudents(@RequestBody StudentsEnroll studentsEnroll) {
        Optional<Subject> subject = subjectService.findById(studentsEnroll.getId_course());
        if (subject.isPresent()) {
            for (StudentsToEnroll student : studentsEnroll.getStudents_to_enroll()) {
                Optional<User> user = userService.findByIdOptional(student.getId_user());
                if (user.isPresent()) {
                    user.get().getSubjects().add(subject.get());
                    userService.save(user.get());
                    subject.get().getUsers().add(user.get());
                    subjectService.save(subject.get());
                }
            }
        }
        return ResponseEntity.status(HttpStatus.OK).body("Students were added successfully");
    }

    @GetMapping(path = "/get-next-courses-for-current-user")
    @ResponseStatus(HttpStatus.OK)
    public String getSubjects() {
        UserSubjectView userSubjectView = userService.findUserNextCourses(userService.getCurrentUserEmail());
        JSONArray jsonArray = new JSONArray();
        ArrayList<DayOfWeek> dayOfWeeks = new ArrayList<>();
        DayOfWeek today = LocalDateTime.now().getDayOfWeek();
        int nextDay;
        for (int i = 0; i < 3; i++) {
            dayOfWeeks.add(today);
            nextDay = today.getValue() + 1;
            today = DayOfWeek.of((nextDay < 5) ? nextDay : 1);
        }
        ArrayList<ScheduleSubjectView> subjectViewArrayList = new ArrayList<>();
        for (SubjectView subjectView : userSubjectView.getSubjects()) {
            for (DayOfWeek day : dayOfWeeks) {
                List<ScheduleSubjectView> subjectViews = scheduleService.getNextSubjects(day, subjectView.getId());
                for (ScheduleSubjectView subjectView1: subjectViews) {
                    if (subjectView1 != null) {
                        subjectViewArrayList.add(subjectView1);
                    }
                }
            }
        }
        subjectViewArrayList.sort((o1, o2) -> {
            if (DayOfWeek.valueOf(o1.getDay()).equals(DayOfWeek.valueOf(o2.getDay()))) {
                return o1.getStartTime().compareTo(o2.getStartTime());
            } else {
                return DayOfWeek.valueOf(o1.getDay()).compareTo(DayOfWeek.valueOf(o2.getDay()));
            }
        });
        for(ScheduleSubjectView subjectView1: subjectViewArrayList) {
            if (jsonArray.size() == 3) {
                break;
            }
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("CourseName", subjectView1.getSubject().getName());
            jsonObject.put("day", subjectView1.getDay());
            jsonObject.put("length", subjectView1.getLength());
            jsonObject.put("startTime", subjectView1.getStartTime().getHour());
            jsonArray.add(jsonObject);
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

    @GetMapping(path = "/get-all-courses-for-current-user")
    @ResponseStatus(HttpStatus.OK)
    public String getAllCoursesForCurrentUser() {
        Optional<User> user = userService.findByEmail(userService.getCurrentUserEmail());
        JSONObject jsonObject = new JSONObject();
        JSONArray courses_enrolled = new JSONArray();
        if (user.isPresent()) {
            for (Subject subject : user.get().getSubjects()) {
                JSONObject course = new JSONObject();
                course.put("id_course", subject.getId());
                course.put("name_course", subject.getName());
                course.put("desc", subject.getInfoSubject());
                course.put("grading", subject.getGradingSubject());
                course.put("name_prof", userService.getProf(subject));
                JSONArray intervals = new JSONArray();
                for (Schedule schedule : subject.getSchedule()) {
                    JSONObject interval = new JSONObject();
                    interval.put("id_interval", schedule.getId());
                    interval.put("day", schedule.getDay());
                    interval.put("length", schedule.getLength());
                    interval.put("start_h", schedule.getStartTime().getHour());
                    intervals.add(interval);
                }
                course.put("Intervals", intervals);
                JSONArray students = new JSONArray();
                for (User student : userService.getStudents(subject)) {
                    students.add(Helper.studentJSON(student));
                }
                course.put("Students_Enrolled", students);
                courses_enrolled.add(course);
            }
        }
        jsonObject.put("count", courses_enrolled.size());
        jsonObject.put("courses_enrolled", courses_enrolled);
        return jsonObject.toString();
    }

    @GetMapping(path = "/teacher/upcoming/courses")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("TEACHER")
    public String getSubjectsBriefVersion() {
        UserSubjectView userSubjectView = userService.findUserNextCourses(userService.getCurrentUserEmail());
        JSONArray jsonArray = new JSONArray();
        ArrayList<DayOfWeek> dayOfWeeks = new ArrayList<>();
        DayOfWeek today = LocalDateTime.now().getDayOfWeek();
        int nextDay;
        for (int i = 0; i < 3; i++) {
            dayOfWeeks.add(today);
            nextDay = today.getValue() + 1;
            today = DayOfWeek.of((nextDay < 5) ? nextDay : 1);
        }
        ArrayList<ScheduleSubjectView> subjectViewArrayList = new ArrayList<>();
        for (SubjectView subjectView : userSubjectView.getSubjects()) {
            for (DayOfWeek day : dayOfWeeks) {
                List<ScheduleSubjectView> subjectViews = scheduleService.getNextSubjects(day, subjectView.getId());
                for (ScheduleSubjectView subjectView1: subjectViews) {
                    if (subjectView1 != null) {
                        subjectViewArrayList.add(subjectView1);
                    }
                }
            }
        }
        subjectViewArrayList.sort((o1, o2) -> {
            if (DayOfWeek.valueOf(o1.getDay()).equals(DayOfWeek.valueOf(o2.getDay()))) {
                return o1.getStartTime().compareTo(o2.getStartTime());
            } else {
                return DayOfWeek.valueOf(o1.getDay()).compareTo(DayOfWeek.valueOf(o2.getDay()));
            }
        });
        for(ScheduleSubjectView subjectView1: subjectViewArrayList){
            if(jsonArray.size() == 3){
                break;
            }
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("name", subjectView1.getSubject().getName());
            String interval = subjectView1.getDay() + " " + subjectView1.getStartTime().getHour() + "." +
                    ((subjectView1.getStartTime().getMinute() < 10) ? (subjectView1.getStartTime().getMinute() + "0") : subjectView1.getStartTime().getMinute()) + "-" +
                    subjectView1.getStartTime().plusHours(subjectView1.getLength()).getHour() + "." +
                    ((subjectView1.getStartTime().plusHours(subjectView1.getLength()).getMinute() < 10) ? (subjectView1.getStartTime().plusHours(subjectView1.getLength()).getMinute() + "0") : subjectView1.getStartTime().plusHours(subjectView1.getLength()).getMinute());

            jsonObject.put("interval", interval);
            jsonArray.add(jsonObject);
        }
        return jsonArray.toJSONString();
    }

    @GetMapping(path = "/teacher/courses/brief")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("TEACHER")
    public String getAllSubjectsBriefVersion() {
        JSONArray jsonArray = new JSONArray();
        for (SubjectView subjectView : subjectService.getAll()) {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("id", subjectView.getId());
            jsonObject.put("subject", subjectView.getName());
            jsonArray.add(jsonObject);
        }
        return jsonArray.toJSONString();
    }

    @GetMapping(path = "/teacher/courses/details")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("TEACHER")
    public String getAllSubjectsDetails() {
        JSONArray jsonArray = new JSONArray();
        for (Subject subject : subjectService.getAllModelView()) {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("id", subject.getId());
            jsonObject.put("title", subject.getName());
            jsonObject.put("description", subject.getInfoSubject());
            JSONArray jsonArray1 = new JSONArray();
            for (Schedule schedule : subject.getSchedule()) {
                JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("id", schedule.getId());
                String interval = schedule.getDay() + " " + schedule.getStartTime().getHour() + "." +
                        ((schedule.getStartTime().getMinute() < 10) ? (schedule.getStartTime().getMinute() + "0") : schedule.getStartTime().getMinute()) + "-" +
                        schedule.getStartTime().plusHours(schedule.getLength()).getHour() + "." +
                        ((schedule.getStartTime().plusHours(schedule.getLength()).getMinute() < 10) ? (schedule.getStartTime().plusHours(schedule.getLength()).getMinute() + "0") : schedule.getStartTime().plusHours(schedule.getLength()).getMinute());
                jsonObject1.put("interval", interval);
                jsonArray1.add(jsonObject1);
            }
            jsonObject.put("intervals", jsonArray1);
            jsonObject.put("grading", subject.getGradingSubject());
            jsonArray.add(jsonObject);
        }
        return jsonArray.toJSONString();
    }
}
