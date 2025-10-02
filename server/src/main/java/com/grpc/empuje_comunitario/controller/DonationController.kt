package com.grpc.empuje_comunitario.controller

import com.grpc.empuje_comunitario.domain.donation.Donation
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.usecases.donationusecase.CreateDonationUseCase
import com.grpc.empuje_comunitario.domain.usecases.donationusecase.UpdateDonationUseCase
import com.grpc.empuje_comunitario.domain.usecases.donationusecase.ListDonationUseCase
import com.grpc.empuje_comunitario.domain.usecases.donationusecase.DisableDonationUseCase
import com.grpc.empuje_comunitario.infrastructure.security.JwtTokenGenerator
import com.grpc.empuje_comunitario.domain.user.Role
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component
import java.time.LocalDateTime
import java.util.UUID

@Component
class DonationController @Autowired constructor(
    private val createDonationUseCase: CreateDonationUseCase,
    private val updateDonationUseCase: UpdateDonationUseCase,
    private val listDonationUseCase: ListDonationUseCase,
    private val disableDonationUseCase: DisableDonationUseCase,
    private val jwtTokenGenerator: JwtTokenGenerator
) {

    private fun validatePresidentOrVocalToken(token: String): String? {
        val result = jwtTokenGenerator.validateAndGetSubjectAndRole(token)
        if (!result.valid) throw IllegalArgumentException("Token inválido o expirado")
        if (result.role != Role.PRESIDENT.toString() && result.role != Role.VOCAL.toString()) {
            throw IllegalAccessException("No autorizado para gestionar inventario de donaciones")
        }
        return result.id
    }

    fun createDonationInventory(
        category: String,
        description: String,
        quantity: Int,
        token: String
    ): MyResult<Unit> {
        return try {
            val userId = validatePresidentOrVocalToken(token)
            createDonationUseCase.invoke(
                Donation(
                    idDonation = UUID.randomUUID().toString(),
                    category = category,
                    description = description,
                    quantity = quantity,
                    isDeleted = false,
                    creationDate = LocalDateTime.now(),
                    creationUser = userId ?: "system"
                )
            )
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    fun updateDonationInventory(
        id: String,
        category: String,
        description: String,
        quantity: Int,
        token: String
    ): MyResult<Donation> {
        return try {
            val userId = validatePresidentOrVocalToken(token)
            val updatedDonation = Donation(
                idDonation = id,
                category = category,
                description = description,
                quantity = quantity,
                isDeleted = false,
                creationDate = LocalDateTime.now(),
                creationUser = userId ?: "system",
                modificationDate = LocalDateTime.now(),
                modificationUser = userId
            )
            updateDonationUseCase.invoke(updatedDonation)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    fun disableDonationInventory(
        donationId: String,
        token: String
    ): MyResult<Unit> {
        return try {
            val userId = validatePresidentOrVocalToken(token)
            disableDonationUseCase.invoke(donationId, userId ?: "system")
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    fun listDonationInventory(
        token: String
    ): MyResult<List<Donation>> {
        return try {
            validatePresidentOrVocalToken(token)
            val donations = listDonationUseCase.invoke()
            MyResult.Success(donations)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}