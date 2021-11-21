package com.mps.QResent.helper;

import com.mps.QResent.model.User;
import net.minidev.json.JSONObject;

public class Helper {
    public static JSONObject studentJSON(User student) {
        JSONObject studentJson = new JSONObject();
        studentJson.put("name", student.getName());
        studentJson.put("secondName", student.getSurname());
        studentJson.put("ldap", student.getEmail());
        studentJson.put("group", student.getGroup());
        studentJson.put("privilege", student.getRole());
        return studentJson;
    }
}
