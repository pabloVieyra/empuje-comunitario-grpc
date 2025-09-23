package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.io.Serializable

@Entity
@Table(name = "user_events")
data class UserEventEntity(
    @Id
    @ManyToOne
    @JoinColumn(name = "user_id", nullable = false)
    val user: UserEntity,

    @Id
    @ManyToOne
    @JoinColumn(name = "event_id", nullable = false)
    val event: EventEntity
) : Serializable
