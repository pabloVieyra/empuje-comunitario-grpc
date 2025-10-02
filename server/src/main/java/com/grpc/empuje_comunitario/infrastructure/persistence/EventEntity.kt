package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.domain.donation.Donation
import com.grpc.empuje_comunitario.domain.donation.Event
import jakarta.persistence.*
import java.time.LocalDateTime

@Entity
@Table(name = "events")
data class EventEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    val id: Int = 0,

    @Column(nullable = false)
    var eventName: String = "",

    @Column(nullable = false)
    var description: String = "",

    @Column(nullable = false)
    var eventDateTime: LocalDateTime,

    @OneToOne
    @JoinColumn(name = "modification_user_id")
    var modificationUser: UserEntity? = null,

    @Column(name = "modification_date")
    var modificationDate: LocalDateTime? = null,

    @OneToMany(mappedBy = "event", cascade = [CascadeType.ALL], orphanRemoval = true)
    val eventDonations: List<EventDonationEntity> = listOf(),

    @OneToMany(mappedBy = "event", cascade = [CascadeType.ALL], orphanRemoval = true)
    val userEvents: List<UserEventEntity> = listOf()
){
    constructor() : this(
        id = 0,
        eventName = "",
        description = "",
        eventDateTime = LocalDateTime.MIN,
        modificationUser = null,
        modificationDate = null,
        eventDonations = listOf(),
        userEvents = listOf()
    )
}
fun EventEntity.toEvent(): Event {
    return Event(
        eventName = this.eventName,
        id =  this.id,
        description = this.description,
        eventDateTime = this.eventDateTime,
        modificationDate = this.modificationDate,
        modificationUser =  modificationUser?.id
    )
}