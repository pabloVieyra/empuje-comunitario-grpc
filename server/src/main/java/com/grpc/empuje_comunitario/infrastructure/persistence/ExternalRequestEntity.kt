package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.time.LocalDateTime

@Entity
@Table(name = "external_requests")
class ExternalRequestEntity(
    @Id
    val id: String = "",

    @Column(name = "requester_org_id", nullable = false)
    val requesterOrgId: String = "",

    @Column(name = "external_request_id", nullable = false, unique = true)
    val externalRequestId: String = "",

    @Column(name = "is_active", nullable = false)
    var isActive: Boolean = true,

    @Column(name = "reception_date", nullable = false)
    val receptionDate: LocalDateTime = LocalDateTime.now(),

    @OneToMany(mappedBy = "request", cascade = [CascadeType.ALL], orphanRemoval = true)
    val details: MutableList<ExternalRequestDetailEntity> = mutableListOf()
) {
    constructor() : this(
        requesterOrgId = "",
        externalRequestId = "",
        receptionDate = LocalDateTime.now()
    )
}