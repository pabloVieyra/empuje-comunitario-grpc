package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*

@Entity
@Table(name = "external_request_details")
class ExternalRequestDetailEntity(
    @Id
    val id: String = "",

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "external_request_id", nullable = false)
    val request: ExternalRequestEntity,

    @Column(nullable = false)
    var category: String = "",

    @Column(nullable = false)
    var description: String = ""
) {
    constructor() : this(request = ExternalRequestEntity())
}