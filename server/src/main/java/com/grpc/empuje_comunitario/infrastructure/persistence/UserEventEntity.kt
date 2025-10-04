package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.io.Serializable

@Entity
@Table(name = "user_events")
class UserEventEntity() : Serializable {
    @Id
    @ManyToOne
    @JoinColumn(name = "user_id", nullable = false)
    lateinit var user: UserEntity

    @Id
    @ManyToOne
    @JoinColumn(name = "event_id", nullable = false)
    lateinit var event: EventEntity


    constructor(user: UserEntity, event: EventEntity) : this() {
        this.user = user
        this.event = event
    }
}