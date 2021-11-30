package com.mps.QResent.helper;

import com.mps.QResent.model.User;
import net.minidev.json.JSONObject;

import java.util.Date;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.TimeZone;

public class Helper {
    public static JSONObject studentJSON(User student) {
        JSONObject studentJson = new JSONObject();
        studentJson.put("name", student.getName());
        studentJson.put("secondName", student.getSurname());
        studentJson.put("ldap", student.getUsername());
        studentJson.put("group", student.getGroup());
        studentJson.put("privilege", student.getRole().ordinal());
        return studentJson;
    }

    public static Date StringToDate() throws ParseException {
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Date date = new Date();
        formatter.setTimeZone(TimeZone.getTimeZone("Europe/Bucharest"));
        return formatter.parse(formatter.format(date));
    }
}
