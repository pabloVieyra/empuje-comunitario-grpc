package com.grpc.empuje_comunitario.domain.usecases.donationusecase

import com.grpc.empuje_comunitario.domain.DonationRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Donation
import com.grpc.empuje_comunitario.domain.user.User
import org.springframework.stereotype.Component

@Component
class UpdateDonationUseCase(
    private val donationRepository: DonationRepository
) {
    operator fun invoke(donation: Donation): MyResult<Donation> {
        return try {
            val result = donationRepository.update(donation)
            if (result is MyResult.Success) {
                MyResult.Success(result.data)
            } else {
                MyResult.Failure(Exception("[PERSISTENT]Failed to update the user in the repository."))
            }
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}