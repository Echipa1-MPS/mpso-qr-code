package com.mps.QResent.service;

import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;
import com.mps.QResent.repository.ScheduleRepository;
import org.springframework.stereotype.Service;

import java.time.DayOfWeek;
import java.util.List;

@Service
public class ScheduleService {
    ScheduleRepository scheduleRepository;

    public ScheduleService(ScheduleRepository scheduleRepository) {
        this.scheduleRepository = scheduleRepository;
    }

    public void save(Schedule schedule){
        scheduleRepository.save(schedule);
    }

    public List<ScheduleSubjectView> getNextSubjects(DayOfWeek day, Long subjectId){
        return scheduleRepository.findAllByDayAfterAndSubject_Id(day, subjectId);
    }

    public List<ScheduleSubjectView> getSubjects(Subject subject){
        return scheduleRepository.findAllBySubject(subject);
    }

    public Schedule getById(Long id){
        return scheduleRepository.getById(id);
    }
}
