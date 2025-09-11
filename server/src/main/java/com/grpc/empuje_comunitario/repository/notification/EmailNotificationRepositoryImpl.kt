package com.grpc.empuje_comunitario.repository.notification

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.notification.EmailNotificationRepository
import com.grpc.empuje_comunitario.repository.notification.EmailGateway
import org.springframework.stereotype.Repository

@Repository
open class EmailNotificationRepositoryImpl(
    private val emailGateway: EmailGateway
) : EmailNotificationRepository {
    override fun sendEmail(to: String, subject: String, body: String): MyResult<Unit>  {
        try {
            emailGateway.sendEmail(to, subject, body)
            return MyResult.Success(Unit)
        }
        catch (e: Exception){
            return MyResult.Failure(e)
        }
    }
}