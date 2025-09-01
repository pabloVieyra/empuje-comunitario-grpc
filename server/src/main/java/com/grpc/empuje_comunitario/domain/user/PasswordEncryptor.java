package com.grpc.empuje_comunitario.domain.user;

public interface PasswordEncryptor {
    String encrypt(String plainPassword);
}