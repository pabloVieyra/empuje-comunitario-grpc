package com.grpc.empuje_comunitario.domain.notification;

import com.grpc.empuje_comunitario.domain.MyResult
import kotlin.Unit;

interface EmailNotificationRepository {
    fun sendEmail(to: String,subject: String,body: String): MyResult<Unit>
}