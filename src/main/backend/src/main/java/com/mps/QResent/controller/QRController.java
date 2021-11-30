package com.mps.QResent.controller;

import com.mps.QResent.helper.Helper;
import com.mps.QResent.model.KeyQr;
import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.service.KeyQrService;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import com.mps.QResent.service.SubjectService;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;
import java.util.Map;
import java.util.Optional;
import java.util.concurrent.TimeUnit;

@CrossOrigin(maxAge = 3600)
@RestController
@RequestMapping(path = "/qr")
public class QRController {

    @Autowired
    private QRCodeService qrService;

    @Autowired
    private SubjectService subjectService;

    @Autowired
    private KeyQrService keyQrService;

    @Autowired
    private ScheduleService scheduleService;

    @PostMapping(path = "/teacher/generate-qr-id")
    @RolesAllowed("TEACHER")
    public ResponseEntity<?> generateQrId(@RequestBody Map<String, Object> request) {

        try {
            if (qrService.areValidCredentials(request)) {
                Long scheduleId = Long.parseLong(String.valueOf(request.get("schedule")));
                Optional<Schedule> schedule = scheduleService.findById(scheduleId);
                if (schedule.isEmpty()) {
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified interval does not exist!");
                }
                QRCode qrCode = new QRCode();
                Long subjectId = Long.parseLong(String.valueOf(request.get("subject")));
                Optional<Subject> subject = subjectService.findById(subjectId);
                if (subject.isEmpty()) {
                    return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified course does not exist!");
                }

                KeyQr keyQr = new KeyQr();
                keyQr.setSubject(subject.get());
                keyQr.setKeyValue(Integer.parseInt(String.valueOf(request.get("key"))));

                keyQrService.save(keyQr);
                subject.get().setKeyQr(keyQr);
                subjectService.save(subject.get());

                qrCode.setSchedule(schedule.get());

                java.util.Date date = Helper.StringToDate();
                java.sql.Timestamp sqlTime = new java.sql.Timestamp(date.getTime());
                qrCode.setDate(sqlTime);

                int offset = Integer.parseInt(String.valueOf(request.get("offset")));
                int reps = Integer.parseInt(String.valueOf(request.get("reps")));
                qrCode.setDateFinish(new java.sql.Timestamp(sqlTime.getTime() + reps * TimeUnit.MINUTES.toMillis(offset)));

                qrService.save(qrCode);

                JSONObject response = new JSONObject();
                response.put("qr_id", qrCode.getId());
                return ResponseEntity.status(HttpStatus.CREATED).body(response);
            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }

    }

    @PatchMapping(path = "/teacher/update-qr-key")
    @RolesAllowed("TEACHER")
    public ResponseEntity<?> updateQrKey(@RequestBody Map<String, Object> request) {
        try {

            if (request.get("qr_id") != null && request.get("key") != null) {
                QRCode qrCode = qrService.findById(Long.parseLong(String.valueOf(request.get("qr_id"))));
                KeyQr key = qrCode.getSchedule().getSubject().getKeyQr();
                key.setKeyValue(Integer.parseInt(String.valueOf(request.get("key"))));
                keyQrService.save(key);
                return ResponseEntity.status(HttpStatus.OK).body("The key was successfully updated!");

            } else {
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Missing required credentials!");
            }
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

}
