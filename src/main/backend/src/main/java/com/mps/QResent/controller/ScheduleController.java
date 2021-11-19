package com.mps.QResent.controller;

import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.User;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping(path = "/schedule")
public class ScheduleController {
    @Autowired
    ScheduleService scheduleService;

    @Autowired
    QRCodeService qrCodeService;

    @GetMapping(path = "/getORUsers/{id}")
    public String getQRListUsers(@PathVariable Long id){
        Schedule schedule = scheduleService.getById(id);
        List<QRCode> qrCodes = qrCodeService.findAllBySchedule(schedule);
        JSONObject jsonObject = new JSONObject();
        JSONArray jsonArray = new JSONArray();
        for(QRCode qrCode: qrCodes){
            for(User user: qrCode.getUsers()){
                jsonArray.add(UserController.studentJSON(user));
            }
        }
        jsonObject.put("QR", jsonArray);
        return jsonObject.toString();
    }
}
