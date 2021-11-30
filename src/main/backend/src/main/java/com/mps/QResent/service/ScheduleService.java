package com.mps.QResent.service;

import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;
import com.mps.QResent.repository.ScheduleRepository;
import org.springframework.stereotype.Service;

import java.time.DayOfWeek;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@Service
public class ScheduleService {
    ScheduleRepository scheduleRepository;

    public ScheduleService(ScheduleRepository scheduleRepository) {
        this.scheduleRepository = scheduleRepository;
    }

    public void save(Schedule schedule){
        scheduleRepository.save(schedule);
    }

    public Optional<Schedule> findById(Long id) {
        return this.scheduleRepository.findById(id);
    }

    public List<ScheduleSubjectView> getNextSubjects(DayOfWeek day, Long subjectId){
        return scheduleRepository.findAllByDayAfterAndSubject_Id(day, subjectId);
    }

    public List<ScheduleSubjectView> getSubjects(Subject subject){
        return scheduleRepository.findAllBySubject(subject);
    }

    public Schedule findByScheduleInfo(Long subjectId, LocalTime start, DayOfWeek day) {
        return this.scheduleRepository.findByScheduleInfo(subjectId, start, day);
    }


    public boolean areValidCredentials(Map<String, Object> request) {
        return (request.get("day") != null)
                && (request.get("duration") != null)
                && (request.get("subject") != null)
                && (request.get("start_time") != null);
    }
}
