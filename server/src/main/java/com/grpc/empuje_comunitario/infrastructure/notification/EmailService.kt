package com.grpc.empuje_comunitario.infrastructure.notification

import com.grpc.empuje_comunitario.domain.user.EmailNotificationService
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.mail.SimpleMailMessage
import org.springframework.mail.javamail.JavaMailSender
import org.springframework.stereotype.Service
import org.slf4j.Logger
import org.slf4j.LoggerFactory

@Service
class EmailService @Autowired constructor(
        private val mailSender: JavaMailSender
) : EmailNotificationService {

    private val logger: Logger = LoggerFactory.getLogger(EmailService::class.java)

    override fun sendEmail(to: String, subject: String, message: String) {
        val mailMessage = SimpleMailMessage().apply {
            setTo(to)
            setFrom("noreply@empujecomunitario.org")
            setSubject(subject)
            setText(message)
        }

        logger.info("Sending email to: {}, subject: {}", to, subject)
        mailSender.send(mailMessage)
        logger.info("Email sent successfully")
    }
}