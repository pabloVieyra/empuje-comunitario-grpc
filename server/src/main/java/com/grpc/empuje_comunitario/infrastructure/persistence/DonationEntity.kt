package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.domain.donation.Donation
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.FetchType
import jakarta.persistence.Id
import jakarta.persistence.JoinColumn
import jakarta.persistence.ManyToOne
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

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "creation_user_id", nullable = false)
    var creationUser: UserEntity,

    var modificationDate: LocalDateTime? = null,
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "modification_user_id")
    var modificationUser: UserEntity?


)
{

}
fun DonationEntity.toDonation(): Donation {
    return Donation(
        idDonation = this.id,
        category = this.category,
        description = this.description,
        quantity = this.quantity,
        isDeleted = this.isDeleted,
        creationDate = this.creationDate,
        creationUser = this.creationUser.id,
        modificationDate = this.modificationDate,
        modificationUser = this.modificationUser?.id
    )
}