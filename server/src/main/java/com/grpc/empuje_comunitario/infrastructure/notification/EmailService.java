package com.grpc.empuje_comunitario.infrastructure.notification;

import com.grpc.empuje_comunitario.domain.user.EmailNotificationService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.stereotype.Service;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Service
public class EmailService implements EmailNotificationService {

    private static final Logger logger = LoggerFactory.getLogger(EmailService.class);
    private final JavaMailSender mailSender;

    @Autowired
    public EmailService(JavaMailSender mailSender) {
        this.mailSender = mailSender;
    }

    @Override
    public void sendEmail(String to, String subject, String message) {
        SimpleMailMessage mailMessage = new SimpleMailMessage();
        mailMessage.setTo(to);
        mailMessage.setFrom("noreply@empujecomunitario.org");
        mailMessage.setSubject(subject);
        mailMessage.setText(message);

        logger.info("Sending email to: {}, subject: {}", to, subject);
        mailSender.send(mailMessage);
        logger.info("Email sent successfully");
    }
}