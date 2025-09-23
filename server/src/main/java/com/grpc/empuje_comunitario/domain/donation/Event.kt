package com.grpc.empuje_comunitario.domain.donation
import com.grpc.empuje_comunitario.infrastructure.persistence.DonationEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.EventEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import java.time.LocalDateTime
class Event(
    val id: Int,
    val eventName: String,
    val description: String,
    val eventDateTime: LocalDateTime,
    val modificationUser: String? ,
    val modificationDate: LocalDateTime? = null
){

}
fun Event.toEventEntity(user: UserEntity): EventEntity {
    return EventEntity(
        eventDateTime = this.eventDateTime,
        description = this.description,
        id = this.id,
        eventName =  this.eventName,
        modificationUser = user,
        modificationDate = this.modificationDate
    )
}