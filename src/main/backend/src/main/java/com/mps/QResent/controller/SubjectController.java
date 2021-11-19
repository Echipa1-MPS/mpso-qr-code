package com.mps.QResent.controller;

import com.mps.QResent.dto.StudentsEnroll;
import com.mps.QResent.dto.StudentsToEnroll;
import com.mps.QResent.dto.SubjectDTO;
import com.mps.QResent.dto.SubjectDTOUpdate;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.projection.SubjectView;
import com.mps.QResent.service.SubjectService;
import com.mps.QResent.service.UserService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;

import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping(path = "/subject")
public class SubjectController {
    @Autowired
    SubjectService subjectService;

    @Autowired
    UserService userService;

    @GetMapping(path = "/getAll")
    @RolesAllowed("ADMIN")
    public List<SubjectView> getAll(){
        return subjectService.getAll();
    }

    @GetMapping(path = "/getAllCourses")
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

    @DeleteMapping(path = "/deleteCourse/{id}")
    @ResponseStatus(HttpStatus.OK)
    @RolesAllowed("ADMIN")
    public void deleteSubject(@PathVariable Long id){
        Optional<Subject> subject = subjectService.findById(id);
        subject.ifPresent(value -> subjectService.delete(value));
    }

    @PostMapping(path = "/createCourse")
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

    @PostMapping(path = "/updateCourse")
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

    @PostMapping(path = "/enrollStudents")
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
}
