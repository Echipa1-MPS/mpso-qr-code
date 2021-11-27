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
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.annotation.security.RolesAllowed;
import java.util.Map;
import java.util.Optional;
import java.util.concurrent.TimeUnit;

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
                keyQr.setKeyValue(Integer.parseInt(String.valueOf(request.get("keyValue"))));

                keyQrService.save(keyQr);

                qrCode.setSchedule(schedule.get());

                java.util.Date date = Helper.StringToDate(String.valueOf(request.get("date")));
                // TODO: verify if it's in the right interval
                java.sql.Timestamp sqlTime = new java.sql.Timestamp(date.getTime());
                qrCode.setDate(sqlTime);

                int offset = Integer.parseInt(String.valueOf(request.get("offset")));
                qrCode.setDateFinish(new java.sql.Timestamp(sqlTime.getTime() + TimeUnit.MINUTES.toMillis(offset)));

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

}
