package com.mps.QResent.controller;

import com.mps.QResent.dto.ScheduleDateDTO;
import com.mps.QResent.helper.Helper;
import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.Subject;
import com.mps.QResent.model.User;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import com.mps.QResent.service.SubjectService;
import com.mps.QResent.service.UserService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.annotation.security.RolesAllowed;
import java.text.SimpleDateFormat;
import java.time.DayOfWeek;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.*;

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

    @Autowired
    UserService userService;


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

    @PostMapping(path = "/get-qr-users")
    public ResponseEntity<?> getQRListUsers(@RequestBody ScheduleDateDTO scheduleDateDTO) {
        try {
            Long id = scheduleDateDTO.getId();
            String date = scheduleDateDTO.getDate();
            Optional<Schedule> schedule = scheduleService.findById(id);
            if (schedule.isEmpty()) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("The specified interval does not exist!");
            }
            List<QRCode> qrCodes = qrCodeService.findAllBySchedule(schedule.get());
            Date timestamp;
            SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
            try {
                timestamp = dateFormat.parse(date);
            } catch(Exception e) {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body("Invalid format date, format is yyyy-MM-dd"+ e.getMessage());
            }
            JSONObject jsonObject = new JSONObject();
            JSONArray jsonArray1 = new JSONArray();
            HashMap<User, Integer> userHashMap  = new HashMap<>();
            Integer qrNumber = 0;
            for (QRCode qrCode : qrCodes) {
                Date qrCodeDate = dateFormat.parse(qrCode.getDate().toString());
                if(qrCodeDate.equals(timestamp)) {
                    ++qrNumber;
                    JSONArray jsonArray = new JSONArray();
                    for (User user : qrCode.getUsers()) {
                        jsonArray.add(Helper.studentJSON(user));
                        if(userHashMap.containsKey(user)){
                            userHashMap.put(user, userHashMap.get(user) + 1);
                        }else {
                            userHashMap.put(user, 1);
                        }
                    }
                    jsonArray1.add(jsonArray);
                }
            }
            Integer fullStrike = 0;
            for(Map.Entry<User, Integer> entry: userHashMap.entrySet()){
                if(entry.getValue() >= qrNumber){
                    ++fullStrike;
                }
            }
            jsonObject.put("full-strike", fullStrike);
            Long absent =  userService.getStudents(schedule.get().getSubject()).stream().filter(user -> !userHashMap.containsKey(user)).count();
            jsonObject.put("absent", absent);
            jsonObject.put("list_qr_attendance", jsonArray1);
            return ResponseEntity.status(HttpStatus.OK).body(jsonObject);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body(e.getMessage());
        }
    }

    @PostMapping(path = "/get-dates-for-intervals")
    public String getDates(@RequestBody Map<String, Object> request){
        JSONArray jsonArray = new JSONArray();
        SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
        ArrayList<Integer> idIntervals = (ArrayList<Integer>) request.get("id_intervals");
        for(Integer id: idIntervals){
            Optional<Schedule> schedule = scheduleService.findById(Long.valueOf(id));
            if(schedule.isPresent()){
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("id_interval", schedule.get().getId());
                JSONArray jsonArray1 = new JSONArray();
                Set<Date> dates = new HashSet<>();
                for(QRCode qrCode: schedule.get().getQrCodes()){
                    try {
                        dates.add(dateFormat.parse(qrCode.getDate().toString()));
                    }
                    catch (Exception ignored){
                    }

                }
                for(Date date: dates){
                    JSONObject jsonObject1 = new JSONObject();
                    jsonObject1.put("date", dateFormat.format(date));
                    jsonArray1.add(jsonObject1);
                }
                System.out.println(jsonArray1);
                jsonObject.put("list_of_dates", jsonArray1);
                jsonArray.add(jsonObject);
            }
        }
        return jsonArray.toJSONString();
    }

}
