package com.grpc.empuje_comunitario.domain.user;

public interface EmailNotificationService {
    void sendEmail(String to, String subject, String message);
}