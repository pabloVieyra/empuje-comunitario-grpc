package com.grpc.empuje_comunitario.domain.donation

import com.grpc.empuje_comunitario.infrastructure.persistence.DonationEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import java.time.LocalDateTime

// Excepción de dominio personalizada
class InvalidDonationQuantityException(message: String) : RuntimeException(message)

class Donation(
    val idDonation: String,
    val category: String,
    val description: String,
    val quantity: Int,
    var isDeleted: Boolean,
    val creationDate: LocalDateTime = LocalDateTime.now(),
    val creationUser: String,
    val modificationDate: LocalDateTime? = null,
    val modificationUser: String? = null
) {
    init {
        if (quantity < 0) {
            throw InvalidDonationQuantityException("No se puede crear una donación con cantidad negativa: $quantity")
        }
    }

    constructor() : this("", "", "", 0, false, LocalDateTime.now(), "", null, null)
}

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