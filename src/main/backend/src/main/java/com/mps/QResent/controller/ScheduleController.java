package com.mps.QResent.controller;

import com.mps.QResent.helper.Helper;
import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import com.mps.QResent.service.SubjectService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;
import java.time.DayOfWeek;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@CrossOrigin(maxAge = 3600)
@RestController
@RequestMapping(path = "/schedule")
public class ScheduleController {
    @Autowired
    SubjectService subjectService;

    @Autowired
    ScheduleService scheduleService;

    @Autowired
    QRCodeService qrCodeService;


    @PostMapping(path = "/admin/add-schedule")
    @RolesAllowed("ADMIN")
    public ResponseEntity<?> addSchedule(@RequestBody Map<String, Object> request) {
        try {
            if (scheduleService.areValidCredentials(request)) {
                Schedule schedule = new Schedule();
                schedule.setDay(DayOfWeek.of(Integer.parseInt(String.valueOf(request.get("day")))));
                schedule.setLength(Integer.parseInt(String.valueOf(request.get("duration"))));
                schedule.setStartTime(LocalTime.parse(String.valueOf(request.get("start_time")), DateTimeFormatter.ofPattern("HH:mm:ss")));
                Long subjectId = Long.parseLong(String.valueOf(request.get("subject")));
                Optional<Subject> subject = subjectService.findById(subjectId);
                if (subject.isEmpty()) {
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified course does not exist!");
                }
                schedule.setSubject(subject.get());

                // Verify if the schedule record already exists
                if (scheduleService.findByScheduleInfo(subjectId, schedule.getStartTime(), schedule.getDay()) != null) {
                    return ResponseEntity.status(HttpStatus.CONFLICT).body("A course is already set for this interval! Choose another one!");
                } else {
                    scheduleService.save(schedule);
                    return ResponseEntity.status(HttpStatus.CREATED).body("A new interval is a motherfucker!");
                }
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
            }

        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    //work in progress, needs update after scan_qr function
    @GetMapping(path = "/teacher/get-qr-users/{id}")
    @RolesAllowed("TEACHER")
    public ResponseEntity<?> getQRListUsers(@PathVariable Long id) {
        try {
            Optional<Schedule> schedule = scheduleService.findById(id);
            if (schedule.isEmpty()) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified interval does not exist!");
            }
            List<QRCode> qrCodes = qrCodeService.findAllBySchedule(schedule.get());
            JSONObject jsonObject = new JSONObject();
            JSONArray jsonArray1 = new JSONArray();
            for (QRCode qrCode : qrCodes) {
                JSONArray jsonArray = new JSONArray();
                for (User user : qrCode.getUsers()) {
                    jsonArray.add(Helper.studentJSON(user));
                }
                jsonArray1.add(jsonArray);
            }
            jsonObject.put("QR", jsonArray1);
            return ResponseEntity.status(HttpStatus.OK).body(jsonObject);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }
}
