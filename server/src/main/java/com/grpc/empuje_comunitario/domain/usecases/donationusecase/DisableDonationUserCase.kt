package com.grpc.empuje_comunitario.domain.usecases.donationusecase

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.DonationRepository
import com.grpc.empuje_comunitario.domain.donation.Donation
import org.springframework.stereotype.Component
import java.time.LocalDateTime

@Component
class DisableDonationUseCase(
    private val donationRepository: DonationRepository
) {
    fun invoke(donationId: String, modificationUser: String): MyResult<Unit> {
        val result = donationRepository.findById(donationId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENT]Failed to fetch the donation from the repository.")
        }
        val oldDonation = result.data

        val disabledDonation = Donation(
            idDonation = oldDonation.idDonation,
            category = oldDonation.category,
            description = oldDonation.description,
            quantity = oldDonation.quantity,
            isDeleted = true,
            creationDate = oldDonation.creationDate,
            creationUser = oldDonation.creationUser,
            modificationDate = LocalDateTime.now(),
            modificationUser = modificationUser
        )

        return try {
            donationRepository.update(disabledDonation)
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}