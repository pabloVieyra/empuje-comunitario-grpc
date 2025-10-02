package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.infrastructure.persistence.DonationEntity

interface DonationNetworkDatabase {
    fun saveDonation(donation: DonationEntity): Boolean
    fun findAll(): List<DonationEntity>
    fun findById(id: String): DonationEntity?
    fun findByCategory(category: String): List<DonationEntity>
    fun updateDonation(donation: DonationEntity): DonationEntity

}
