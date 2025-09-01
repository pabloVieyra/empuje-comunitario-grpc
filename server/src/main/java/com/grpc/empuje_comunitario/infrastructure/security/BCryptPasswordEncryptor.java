package com.grpc.empuje_comunitario.infrastructure.security;

import com.grpc.empuje_comunitario.domain.user.PasswordEncryptor;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Component;

@Component
public class BCryptPasswordEncryptor implements PasswordEncryptor {

    private final BCryptPasswordEncoder encoder = new BCryptPasswordEncoder();

    @Override
    public String encrypt(String plainPassword) {
        return encoder.encode(plainPassword);
    }

}