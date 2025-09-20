package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.repository.DonationNetworkDatabase
import jakarta.persistence.EntityManager
import jakarta.persistence.PersistenceContext
import org.slf4j.Logger
import org.slf4j.LoggerFactory

import org.springframework.stereotype.Service

@Service
open class PostgresSqlDonationRepository: DonationNetworkDatabase {

    private val logger: Logger = LoggerFactory.getLogger(PostgreSQLUserRepository::class.java)

    @PersistenceContext
    private lateinit var entityManager: EntityManager

    override fun saveDonation(donation: DonationEntity): Boolean {

        try {
            entityManager.persist(donation)
            entityManager.flush()
            return true
        } catch (e: Exception) {
            logger.error("Database error while saving user: ${e.message}")
            throw e
        }
    }

    override fun findAll(): List<DonationEntity> {
        return try {
            val query = entityManager.createQuery(
                "SELECT d FROM DonationEntity d WHERE d.isDeleted = false",
                DonationEntity::class.java
            )
            query.resultList
        } catch (e: Exception) {
            logger.error("Database error while fetching donations: ${e.message}")
            throw e
        }
    }


    override fun findById(id: String): DonationEntity? {
        return try {
            entityManager.find(DonationEntity::class.java, id)
        } catch (e: Exception) {
            logger.error("Database error while finding donation by id: ${e.message}")
            throw e
        }
    }

    override fun findByCategory(category: String): List<DonationEntity> {
        return try {
            val query = entityManager.createQuery(
                "SELECT d FROM DonationEntity d WHERE d.category = :category AND d.isDeleted = false",
                DonationEntity::class.java
            )
            query.setParameter("category", category)
            query.resultList
        } catch (e: Exception) {
            logger.error("Database error while fetching donations by category: ${e.message}")
            throw e
        }
    }
    override fun updateDonation(donation: DonationEntity): DonationEntity {
        return try {
            val existing = entityManager.find(DonationEntity::class.java, donation.id)
                ?: throw IllegalArgumentException("Donation not found with id: ${donation.id}")

            existing.apply {
                description = donation.description
                quantity = donation.quantity
                isDeleted = donation.isDeleted
                modificationDate = donation.modificationDate
                modificationUser = donation.modificationUser
            }

            entityManager.merge(existing)
            entityManager.flush()
            existing
        } catch (e: Exception) {
            logger.error("Database error while updating donation: ${e.message}")
            throw e
        }
    }

}