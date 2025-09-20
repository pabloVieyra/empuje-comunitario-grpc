package com.grpc.empuje_comunitario.domain.donation
import java.time.LocalDateTime
class Event(
    val id: Int,
    val eventName: String,
    val description: String,
    val eventDateTime: LocalDateTime,
)