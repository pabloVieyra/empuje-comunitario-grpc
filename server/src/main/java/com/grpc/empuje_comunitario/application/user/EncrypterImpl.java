package com.grpc.empuje_comunitario.application.user;

import com.grpc.empuje_comunitario.infrastructure.security.BCryptPasswordEncryptor;
import org.springframework.stereotype.Component;

@Component
public class EncrypterImpl implements Encrypter {

    private final BCryptPasswordEncryptor encryptor;

    public EncrypterImpl(
            BCryptPasswordEncryptor encryptor
    ) {
        this.encryptor = encryptor;
    }

    @Override
    public String encrypt(String data) {
        return encryptor.encrypt(data);
    }
}
