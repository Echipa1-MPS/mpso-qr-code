package com.mps.QResent.controller;

import com.mps.QResent.projection.SubjectView;
import com.mps.QResent.service.SubjectService;
import net.bytebuddy.agent.builder.AgentBuilder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping(path = "/subject")
public class SubjectController {
    @Autowired
    SubjectService subjectService;

    @GetMapping(path = "/getAll")
    public List<SubjectView> getAll(){
        return subjectService.getAll();
    }
}
