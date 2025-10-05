package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.time.LocalDateTime

@Entity
@Table(name = "external_events")
class ExternalEventEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    val id: Int = 0,

    @Column(name = "organizer_org_id", nullable = false)
    val organizerOrgId: String = "",

    @Column(name = "external_event_id", nullable = false, unique = true)
    val externalEventId: String = "",

    @Column(name = "event_name", nullable = false)
    var eventName: String = "",

    @Column(nullable = false)
    var description: String = "",
    @Column(name = "event_datetime", nullable = false)
    var eventDateTime: LocalDateTime = LocalDateTime.MIN,

    @Column(name = "is_active", nullable = false)
    var isActive: Boolean = true,

    @OneToMany(mappedBy = "event", cascade = [CascadeType.ALL], orphanRemoval = true)
    val adheredVolunteers: MutableList<ExternalAdhesionEntity> = mutableListOf()
) {
    constructor() : this(
        organizerOrgId = "",
        externalEventId = "",
        eventName = "",
        description = "",
        eventDateTime = LocalDateTime.now()
    )
}