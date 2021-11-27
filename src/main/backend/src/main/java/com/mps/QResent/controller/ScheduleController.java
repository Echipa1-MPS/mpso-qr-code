package com.mps.QResent.controller;

import com.mps.QResent.helper.Helper;
import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.User;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import net.minidev.json.JSONArray;
import net.minidev.json.JSONObject;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path = "/schedule")
public class ScheduleController {
    @Autowired
    ScheduleService scheduleService;

    @Autowired
    QRCodeService qrCodeService;

    //work in progress, needs update after create_qr function
    @GetMapping(path = "/get-qr-users/{id}")
    public String getQRListUsers(@PathVariable Long id){
        Schedule schedule = scheduleService.getById(id);
        List<QRCode> qrCodes = qrCodeService.findAllBySchedule(schedule);
        JSONObject jsonObject = new JSONObject();
        JSONArray jsonArray1 = new JSONArray();
        for(QRCode qrCode: qrCodes){
            JSONArray jsonArray = new JSONArray();
            for(User user: qrCode.getUsers()){
                jsonArray.add(Helper.studentJSON(user));
            }
            jsonArray1.add(jsonArray);
        }
        jsonObject.put("QR", jsonArray1);
        return jsonObject.toString();
    }
}
