package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.Id
import jakarta.persistence.Table
import java.time.LocalDateTime


@Entity
@Table(name = "donations")
data class DonationEntity(
    @Id
    val id: String = "",

    @Column(nullable = false)
    var category: String = "",

    @Column(nullable = false)
    var description: String = "",

    @Column(nullable = false)
    var quantity: Int = 0,

    @Column(name = "is_deleted", nullable = false)
    var isDeleted: Boolean = false,

    @Column(nullable = false)
    val creationDate: LocalDateTime = LocalDateTime.now(),

    @Column(nullable = false)
    val creationUser: String = "",

    var modificationDate: LocalDateTime? = null,

    var modificationUser: String? = null
)