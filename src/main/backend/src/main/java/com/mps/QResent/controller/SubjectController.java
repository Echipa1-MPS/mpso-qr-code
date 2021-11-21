package com.mps.QResent.controller;

import com.mps.QResent.dto.StudentsEnroll;
import com.mps.QResent.dto.StudentsToEnroll;
import com.mps.QResent.dto.SubjectDTO;
import com.mps.QResent.dto.SubjectDTOUpdate;
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
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;

import java.time.DayOfWeek;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping(path = "/subject")
public class SubjectController {
    @Autowired
    SubjectService subjectService;

    @Autowired
    UserService userService;

    @Autowired
    private ScheduleService scheduleService;

    @GetMapping(path = "/admin/getAll")
    @RolesAllowed("ADMIN")
    public List<SubjectView> getAll(){
        return subjectService.getAll();
    }

    @GetMapping(path = "/admin/getAllCourses")
    @RolesAllowed("ADMIN")
    public String getAllCourses(){
        List<SubjectView> subjects = subjectService.getAll();
        JSONObject jsonObject = new JSONObject();
        JSONArray jsonArray = new JSONArray();
        for(SubjectView subject: subjects){
            JSONObject jsonObject1 =  new JSONObject();
            jsonObject1.put("Id_Course",subject.getId());
            jsonObject1.put("Name_C", subject.getName());
            subjectService.findById(subject.getId()).ifPresent(value -> jsonObject1.put("Id_Professor", userService.getProfId(value)));
            subjectService.findById(subject.getId()).ifPresent(value -> jsonObject1.put("Id_Professor", userService.getProf(value)));
            jsonObject1.put("Desc", subject.getInfoSubject());
            jsonObject1.put("Grading", subject.getGradingSubject());
            jsonArray.add(jsonObject1);
        }
        jsonObject.put("Courses", jsonArray);
        return jsonObject.toString();
    }

    @DeleteMapping(path = "/admin/deleteCourse/{id}")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public void deleteSubject(@PathVariable Long id){
        Optional<Subject> subject = subjectService.findById(id);
        subject.ifPresent(value -> subjectService.delete(value));
    }

    @PostMapping(path = "/admin/createCourse")
    @RolesAllowed("ADMIN")
    public Long createSubject(@RequestBody SubjectDTO subjectDTO) {
        Subject subject = new Subject();
        subject.setInfoSubject(subjectDTO.getDesc());
        subject.setName(subjectDTO.getNameC());
        subject.setGradingSubject(subjectDTO.getGrading());
        subjectService.save(subject);
        if(userService.findByIdOptional(subjectDTO.getIdProfessor()).isPresent()){
            subject.getUsers().add(userService.findByIdOptional(subjectDTO.getIdProfessor()).get());
            subjectService.save(subject);
        }
        return subject.getId();
    }

    //TODO make like Carmina in UserController with switch
    @PostMapping(path = "/admin/updateCourse")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public void updateSubject(@RequestBody SubjectDTOUpdate subjectDTO){
        Optional<Subject> subject = subjectService.findById(subjectDTO.getId());
        if(subject.isPresent()){
            if(subjectDTO.getDesc() != null){
                subject.get().setInfoSubject(subjectDTO.getDesc());
            }
            if(subjectDTO.getGrading()!= null){
                subject.get().setGradingSubject(subjectDTO.getGrading());
            }
            if(subjectDTO.getNameC() != null){
                subject.get().setName(subjectDTO.getNameC());
            }
            if(subjectDTO.getIdProfessor()!= null){
                Optional<User> user = userService.findByIdOptional(subjectDTO.getId());
                user.ifPresent(value -> subject.get().getUsers().add(value));
            }
            subjectService.save(subject.get());
        }
    }

    @PostMapping(path = "/admin/enrollStudents")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public void enrollStudents(@RequestBody StudentsEnroll studentsEnroll){
        Optional<Subject> subject = subjectService.findById(studentsEnroll.getId_course());
        if(subject.isPresent()){
            for (StudentsToEnroll student: studentsEnroll.getStudents_to_enroll()){
                Optional<User> user = userService.findByIdOptional(student.getId_user());
                if(user.isPresent()){
                    user.get().getSubjects().add(subject.get());
                    userService.save(user.get());
                    subject.get().getUsers().add(user.get());
                    subjectService.save(subject.get());
                }
            }
        }
    }

    @GetMapping(path = "/get-subjects-for-current-user")
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

    @GetMapping(path = "/get-all-courses-for-current-user")
    public String getAllCoursesForCurrentUser(){
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


}
