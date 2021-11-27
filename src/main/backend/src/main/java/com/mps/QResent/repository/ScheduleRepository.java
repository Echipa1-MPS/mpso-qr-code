package com.mps.QResent.repository;

import com.mps.QResent.model.Schedule;
import org.springframework.data.jpa.repository.JpaRepository;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;

import java.time.DayOfWeek;
import java.util.List;
import java.util.Optional;

public interface ScheduleRepository extends JpaRepository<Schedule,Long> {
    List<ScheduleSubjectView> findAllByDayAfterAndSubject_Id(DayOfWeek day, Long id);
    List<ScheduleSubjectView> findAllBySubject(Subject subject);
    Optional<Schedule> findById(Long id);
}
