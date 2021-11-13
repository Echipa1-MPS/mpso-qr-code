package com.mps.QResent;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.crypto.password.PasswordEncoder;

@SpringBootApplication
public class QResentApplication {

	public static void main(String[] args) {
		SpringApplication.run(QResentApplication.class, args);
	}

}
