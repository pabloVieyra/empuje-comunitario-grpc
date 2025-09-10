package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.notification.EmailNotificationRepository
import org.springframework.stereotype.Component

@Component
class SendEmailUseCase(
  private val emailNotificationRepository: EmailNotificationRepository
) {
  operator fun invoke(email: String, subject: String, body: String): MyResult<Unit> {
    return try {
      emailNotificationRepository.sendEmail(email, subject, body)
      MyResult.Success(Unit)
    } catch (e: Exception) {
      MyResult.Failure(e)
    }
  }
}