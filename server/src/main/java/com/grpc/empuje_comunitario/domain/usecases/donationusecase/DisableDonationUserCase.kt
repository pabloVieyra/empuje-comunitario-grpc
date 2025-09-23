package com.grpc.empuje_comunitario.domain.usecases.donationusecase

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.DonationRepository
import org.springframework.stereotype.Component

@Component
class DisableDonationUseCase(
    private val donationRepository: DonationRepository
) {
    fun invoke(donationId: String, modificationUser: String): MyResult<Unit> {
        val result = donationRepository.findById(donationId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENT]Failed to fetch the user from the repository.")
        }
        result.data.isDeleted = true
        val updatedUser = result.data
        return try {
            donationRepository.update(updatedUser)
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}