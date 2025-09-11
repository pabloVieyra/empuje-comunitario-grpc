package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.repository.NetworkDatabase
import jakarta.persistence.EntityManager
import jakarta.persistence.PersistenceContext
import org.slf4j.Logger
import org.slf4j.LoggerFactory
import org.springframework.stereotype.Service

@Service
open class PostgreSQLUserRepository : NetworkDatabase {

    private val logger: Logger = LoggerFactory.getLogger(PostgreSQLUserRepository::class.java)

    @PersistenceContext
    private lateinit var entityManager: EntityManager

    override fun saveUser(userEntity: UserEntity): Boolean {
        try {
            entityManager.persist(userEntity)
            entityManager.flush() // Forces Hibernate to execute SQL now
            return true
        } catch (e: Exception) {
            // Log the exception but rethrow it so higher layers can handle it properly
            logger.error("Database error while saving user: ${e.message}")
            throw e
        }
    }

    override fun findUserByEmail(email: String?): UserEntity? {
        return try {
            val query = entityManager.createQuery(
                "SELECT u FROM UserEntity u WHERE u.email = :email",
                UserEntity::class.java
            )
            query.setParameter("email", email)
            query.resultList.firstOrNull()
        } catch (e: Exception) {
            logger.error("Database error while finding user by email: ${e.message}")
            throw e
        }
    }
}
