package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.DonationRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Donation
import com.grpc.empuje_comunitario.domain.donation.toDonationEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.toDonation
import org.springframework.stereotype.Repository
import java.time.LocalDateTime.now

@Repository
open class DonationRepositoryImpl(
    private val donationNetworkDatabase: DonationNetworkDatabase,
    private val userNetworkDataBase: NetworkDatabase
) : DonationRepository {


    override fun create(donation: Donation): MyResult<Unit> {
        return try {
            val user = userNetworkDataBase.findUserById(donation.creationUser) ?: return MyResult.Failure(Exception("Creation user not found"))
            val success = donationNetworkDatabase.saveDonation(donation.toDonationEntity(user))
            if (success) MyResult.Success(Unit)
            else MyResult.Failure(Exception("Failed to save donation"))
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun findAll(): MyResult<List<Donation>> {
        return try {
            val donations = donationNetworkDatabase.findAll().map { it.toDonation() }
            MyResult.Success(donations)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun update(donation: Donation): MyResult<Donation> {
        return try {
            val userUpdate: UserEntity? = donation.modificationUser?.let { modUserId ->
                userNetworkDataBase.findUserById(modUserId)
                    ?: return MyResult.Failure(Exception("Modification user not found"))
            }
            val userCreation = userNetworkDataBase.findUserById(donation.creationUser) ?: return MyResult.Failure(Exception("Creation user not found"))
            val updated = donationNetworkDatabase.updateDonation(donation.toDonationEntity(userCreation, userUpdate))
            MyResult.Success(updated.toDonation())
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun delete(id: String, modificationUser: String): MyResult<Unit> {
        return try {
            val userUpdate = userNetworkDataBase.findUserById(modificationUser)
                ?: return MyResult.Failure(Exception("Modification user not found"))

            val donation = donationNetworkDatabase.findById(id)
                ?: return MyResult.Failure(Exception("Donation not found"))
            donation.isDeleted = true
            donation.modificationUser = userUpdate
            donation.modificationDate = now()
            donationNetworkDatabase.updateDonation(donation)
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun findById(id: String): MyResult<Donation> {
        return try {
            val donationEntity = donationNetworkDatabase.findById(id)
                ?: return MyResult.Failure(Exception("Donation not found"))
            MyResult.Success(donationEntity.toDonation())
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun findByIsDeletedFalse(): MyResult<List<Donation>> {
        return try {
            val donations = donationNetworkDatabase.findAll()
                .filter { !it.isDeleted }
                .map { it.toDonation() }
            MyResult.Success(donations)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}
