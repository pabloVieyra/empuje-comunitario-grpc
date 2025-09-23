package com.grpc.empuje_comunitario.domain.usecases.donationusecase

import com.grpc.empuje_comunitario.domain.DonationRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Donation

import org.springframework.stereotype.Component

@Component
class CreateDonationUseCase(
    private val donationRepository: DonationRepository,
){
    operator fun invoke(donation: Donation) {
        val donationRepositoryResult = donationRepository.create((donation))
        if (donationRepositoryResult !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to persist the new user in the repository.")
        }
    }
}