// infrastructure/notification/JavaMailEmailGateway.kt
package com.grpc.empuje_comunitario.infrastructure.notification

import com.grpc.empuje_comunitario.repository.notification.EmailGateway
import org.springframework.mail.javamail.JavaMailSender
import org.springframework.mail.SimpleMailMessage
import org.springframework.stereotype.Component

@Component
class JavaMailEmailGateway(
    private val mailSender: JavaMailSender
) : EmailGateway {
    override fun sendEmail(to: String, subject: String, body: String): Boolean {
        return try {
            val mailMessage = SimpleMailMessage().apply {
                setTo(to)
                setFrom("noreply@empujecomunitario.org")
                setSubject(subject)
                setText(body)
            }
            mailSender.send(mailMessage)
            true
        } catch (e: Exception) {
            false
        }
    }
}