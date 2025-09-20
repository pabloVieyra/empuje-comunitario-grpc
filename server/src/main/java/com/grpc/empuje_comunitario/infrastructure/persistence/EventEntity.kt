/*package com.grpc.empuje_comunitario.infrastructure.persistence

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

    @OneToMany(mappedBy = "event", cascade = [CascadeType.ALL], orphanRemoval = true)
    val eventDonations: List<EventDonationEntity> = listOf(),

    @OneToMany(mappedBy = "event", cascade = [CascadeType.ALL], orphanRemoval = true)
    val userEvents: List<UserEventEntity> = listOf()
)*/