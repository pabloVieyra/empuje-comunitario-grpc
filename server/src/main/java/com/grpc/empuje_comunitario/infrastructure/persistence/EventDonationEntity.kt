package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.io.Serializable

@Entity
@Table(name = "event_donations")
data class EventDonationEntity(
    @Id
    @ManyToOne
    @JoinColumn(name = "donation_id", nullable = false)
    val donation: DonationEntity,

    @Id
    @ManyToOne
    @JoinColumn(name = "event_id", nullable = false)
    val event: EventEntity,

    @Column(nullable = false)
    val quantity: Int
) : Serializable
