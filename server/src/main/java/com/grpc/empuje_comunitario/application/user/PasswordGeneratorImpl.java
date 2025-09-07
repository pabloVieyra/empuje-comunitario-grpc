package com.grpc.empuje_comunitario.application.user;

import org.springframework.stereotype.Component;

import java.security.SecureRandom;

@Component
public class PasswordGeneratorImpl implements PasswordGenerator {
    private static final String CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
    private static final SecureRandom random = new SecureRandom();
    private static final int PASSWORD_LENGTH = 10;
    private final Encrypter encrypter;

    public PasswordGeneratorImpl(
            Encrypter encrypter
    ) {
        this.encrypter = encrypter;
    }

    //TODO: Mejorar la seguridad de la generación de contraseñas
    public String generateRandomPassword() {
        StringBuilder sb = new StringBuilder(PASSWORD_LENGTH);
        for (int i = 0; i < PASSWORD_LENGTH; i++) {
            int randomIndex = random.nextInt(CHARS.length());
            sb.append(CHARS.charAt(randomIndex));
        }
        return this.encrypter.encrypt(sb.toString());
    }
}