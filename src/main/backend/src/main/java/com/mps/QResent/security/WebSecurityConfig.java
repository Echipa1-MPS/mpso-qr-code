package com.mps.QResent.security;

import com.mps.QResent.enums.Role;
import com.mps.QResent.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.method.configuration.EnableGlobalMethodSecurity;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.crypto.password.Pbkdf2PasswordEncoder;
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter;

@EnableWebSecurity
@EnableGlobalMethodSecurity(
        securedEnabled = true,
        jsr250Enabled = true,
        prePostEnabled = true
)
@Configuration
public class WebSecurityConfig extends WebSecurityConfigurerAdapter {
    @Autowired
    private UserService userService;

    @Autowired
    private JwtRequestFilter jwtRequestFilter;

    @Override
    public void configure(AuthenticationManagerBuilder auth) throws Exception {
        auth.userDetailsService(userService);
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new Pbkdf2PasswordEncoder("hdrjghskjfiah70j");
    }

    @Override
    @Bean
    public AuthenticationManager authenticationManagerBean() throws Exception {
        return super.authenticationManagerBean();
    }

    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http.csrf().disable()
                .authorizeRequests().antMatchers("api/user/authentication/*").permitAll()
                .and().authorizeRequests().antMatchers("api/user/admin/*").hasRole(String.valueOf(Role.ADMIN))
                .and().authorizeRequests().antMatchers("api/user/student/*").hasRole(String.valueOf(Role.STUDENT))
                .and().authorizeRequests().antMatchers("api/user/teacher/*").hasRole(String.valueOf(Role.TEACHER))
                .and().authorizeRequests().antMatchers("api/subject/admin/*").hasRole(String.valueOf(Role.ADMIN))
                .and().authorizeRequests().antMatchers("api/subject/student/*").hasRole(String.valueOf(Role.STUDENT))
                .and().authorizeRequests().antMatchers("api/subject/teacher/*").hasRole(String.valueOf(Role.TEACHER))
                .and().authorizeRequests().antMatchers("api/schedule/admin/*").hasRole(String.valueOf(Role.ADMIN))
                .and().authorizeRequests().antMatchers("api/schedule/teacher/*").hasRole(String.valueOf(Role.TEACHER))
                .and().authorizeRequests().antMatchers("api/qr/teacher/*").hasRole(String.valueOf(Role.TEACHER))
                .and().authorizeRequests().antMatchers("api/qr/student/*").hasRole(String.valueOf(Role.STUDENT))
                .and().sessionManagement()
                .sessionCreationPolicy(SessionCreationPolicy.STATELESS);

        http.addFilterBefore(jwtRequestFilter, UsernamePasswordAuthenticationFilter.class);
    }
}