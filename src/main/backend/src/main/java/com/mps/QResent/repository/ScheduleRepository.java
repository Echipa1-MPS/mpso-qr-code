package com.mps.QResent.repository;

import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;
import org.springframework.data.jpa.repository.JpaRepository;

import java.time.DayOfWeek;
import java.util.Date;
import java.util.List;

public interface ScheduleRepository extends JpaRepository<Schedule,Long> {
    List<ScheduleSubjectView> findAllByDayAfterAndSubject_Id(DayOfWeek day, Long id);
    List<ScheduleSubjectView> findAllBySubject(Subject subject);
}
