package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.domain.donation.Donation
import java.time.LocalDateTime
import jakarta.persistence.*

@Entity
@Table(name = "donations")
class DonationEntity(
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

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "creation_user_id", nullable = false)
    var creationUser: UserEntity? = null,

    var modificationDate: LocalDateTime? = null,

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "modification_user_id")
    var modificationUser: UserEntity? = null
) {
    constructor() : this("", "", "", 0, false, LocalDateTime.now(), null, null, null)
}

// Mapper Entity -> Domain
fun DonationEntity.toDonation(): Donation {
    return Donation(
        idDonation = this.id,
        category = this.category,
        description = this.description,
        quantity = this.quantity,
        isDeleted = this.isDeleted,
        creationDate = this.creationDate,
        creationUser = this.creationUser?.id ?: "",
        modificationDate = this.modificationDate,
        modificationUser = this.modificationUser?.id
    )
}

// Mapper Domain -> Entity
fun Donation.toDonationEntity(
    creationUserEntity: UserEntity,
    modificationUserEntity: UserEntity? = null
): DonationEntity {
    return DonationEntity(
        id = this.idDonation,
        category = this.category,
        description = this.description,
        quantity = this.quantity,
        isDeleted = this.isDeleted,
        creationDate = this.creationDate,
        creationUser = creationUserEntity,
        modificationDate = this.modificationDate,
        modificationUser = modificationUserEntity
    )
}