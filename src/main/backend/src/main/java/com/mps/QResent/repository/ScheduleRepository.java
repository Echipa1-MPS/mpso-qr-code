package com.mps.QResent.repository;

import com.mps.QResent.model.Schedule;
import org.springframework.data.jpa.repository.JpaRepository;
import com.mps.QResent.model.Subject;
import com.mps.QResent.projection.ScheduleSubjectView;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.time.DayOfWeek;
import java.time.LocalTime;
import java.util.List;
import java.util.Optional;

public interface ScheduleRepository extends JpaRepository<Schedule,Long> {
    List<ScheduleSubjectView> findAllByDayAfterAndSubject_Id(DayOfWeek day, Long id);
    List<ScheduleSubjectView> findAllBySubject(Subject subject);
    Optional<Schedule> findById(Long id);
    @Query(value = "SELECT * FROM schedules s WHERE s.subject_id = :subject and s.start_hour = :#{#start.toString()} and s.date = :#{#day.getValue()}", nativeQuery = true)
    Schedule findByScheduleInfo(@Param("subject") Long subject, @Param("start") LocalTime start, @Param("day") DayOfWeek day);
}
