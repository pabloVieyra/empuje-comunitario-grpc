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
            entityManager.flush()
            return true
        } catch (e: Exception) {
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

    override fun findAllUsers(): List<UserEntity> {
        return try {
            val query = entityManager.createQuery("SELECT u FROM UserEntity u", UserEntity::class.java)
            query.resultList
        } catch (e: Exception) {
            logger.error("Database error while fetching all users: ${e.message}")
            throw e
        }
    }

    override fun updateUser(user: UserEntity): UserEntity {
        return try {
            val existingUser = entityManager.find(UserEntity::class.java, user.id)
            if (existingUser != null) {
                existingUser.apply {
                    this.username = user.username
                    this.name = user.name
                    this.lastname = user.lastname
                    this.phone = user.phone
                    this.email = user.email
                    this.role = user.role
                    this.active = user.active
                }
                entityManager.merge(existingUser)
                entityManager.flush()
                existingUser
            } else {
                throw IllegalArgumentException("User not found with id: ${user.id}")
            }
        } catch (e: Exception) {
            logger.error("Database error while updating user: ${e.message}")
            throw e
        }
    }

    override fun findUserById(id: String): UserEntity? {
        return try {
            entityManager.find(UserEntity::class.java, id)
        } catch (e: Exception) {
            logger.error("Database error while finding user by id: ${e.message}")
            throw e
        }
    }
}