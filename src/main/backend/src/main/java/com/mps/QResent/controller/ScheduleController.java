package com.mps.QResent.controller;

import com.mps.QResent.model.QRCode;
import com.mps.QResent.model.Schedule;
import com.mps.QResent.model.User;
import com.mps.QResent.service.QRCodeService;
import com.mps.QResent.service.ScheduleService;
import net.minidev.json.JSONArray;
import org.json.JSONObject;
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
                JSONObject jsonObject1 = new JSONObject();
                jsonObject1.put("name", user.getName());
                jsonObject1.put("secondName", user.getSurname());
                jsonObject1.put("ldap", user.getEmail());
                jsonObject1.put("group", user.getGroup());
                jsonObject1.put("privilege", user.getRole());
                jsonArray.add(jsonObject1);
            }
        }
        jsonObject.put("QR", jsonArray);
        return jsonObject.toString();
    }
}
