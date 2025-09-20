package com.grpc.empuje_comunitario.domain

import com.grpc.empuje_comunitario.domain.donation.Donation

interface DonationRepository {
    fun create(donation: Donation): MyResult<Unit>
    fun findAll(): MyResult<List<Donation>>
    fun update(donation: Donation): MyResult<Donation>
    fun delete(id: String, modificationUser: String): MyResult<Unit>
    fun findById(id: String): MyResult<Donation>
    fun findByIsDeletedFalse(): MyResult<List<Donation>>
}
