package com.grpc.empuje_comunitario.domain.usecases.donationusecase

import com.grpc.empuje_comunitario.domain.DonationRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Donation
import org.springframework.stereotype.Component

@Component
class ListDonationUseCase(
    private val donationRepository: DonationRepository
) {
    fun invoke(): List<Donation> {
        val result = donationRepository.findAll()
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to fetch users from the repository.")
        }
        return result.data
    }
}