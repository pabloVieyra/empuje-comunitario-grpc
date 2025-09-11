package com.grpc.empuje_comunitario.repository.notification

    interface EmailGateway {
        fun sendEmail(to: String, subject: String, body: String): Boolean
    }