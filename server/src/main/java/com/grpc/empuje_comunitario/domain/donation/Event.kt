package com.grpc.empuje_comunitario.domain.donation

import com.grpc.empuje_comunitario.infrastructure.persistence.EventEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import java.time.LocalDateTime

class Event(
    val id: Int,
    val eventName: String,
    val description: String,
    val eventDateTime: LocalDateTime,
    val modificationUser: String?,
    val modificationDate: LocalDateTime? = null
) {
    companion object {
        fun create(eventName: String, description: String, eventDateTime: LocalDateTime): Event {
            require(eventDateTime.isAfter(LocalDateTime.now())) { "La fecha del evento debe ser futura" }
            return Event(
                id = 0, // Se genera en la base de datos
                eventName = eventName,
                description = description,
                eventDateTime = eventDateTime,
                modificationUser = null,
                modificationDate = null
            )
        }
    }
}

fun Event.toEventEntity(user: UserEntity): EventEntity {
    return EventEntity(
        eventDateTime = this.eventDateTime,
        description = this.description,
        id = this.id,
        eventName = this.eventName,
        modificationUser = user,
        modificationDate = this.modificationDate
    )
}