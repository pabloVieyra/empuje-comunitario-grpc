package com.grpc.empuje_comunitario.infrastructure.persistence
import com.grpc.empuje_comunitario.repository.EventNetworkDatabase
import org.slf4j.LoggerFactory
import jakarta.persistence.EntityManager
import jakarta.persistence.PersistenceContext
import org.springframework.stereotype.Service

@Service
open class PostgresSqlEventRepository: EventNetworkDatabase {

    private val logger = LoggerFactory.getLogger(PostgresSqlEventRepository::class.java)

    @PersistenceContext
    private lateinit var entityManager: EntityManager

    override fun saveEvent(event: EventEntity): Boolean = try {
        entityManager.persist(event)
        entityManager.flush()
        true
    } catch (e: Exception) {
        logger.error("Error saving event: ${e.message}")
        throw e
    }

    override fun updateEvent(event: EventEntity): EventEntity = try {
        val existing = entityManager.find(EventEntity::class.java, event.id)
            ?: throw IllegalArgumentException("Event not found with id: ${event.id}")

        existing.apply {
            eventName = event.eventName
            description = event.description
            eventDateTime = event.eventDateTime
            modificationUser = event.modificationUser
            modificationDate = event.modificationDate
        }

        entityManager.merge(existing)
        entityManager.flush()
        existing
    } catch (e: Exception) {
        logger.error("Error updating event: ${e.message}")
        throw e
    }

    override fun findById(id: Int): EventEntity? = try {
        entityManager.find(EventEntity::class.java, id)
    } catch (e: Exception) {
        //logger.error("Error finding event by id: ${e.message}")
        throw e
    }

    override fun findAll(): List<EventEntity> = try {
        val query = entityManager.createQuery("SELECT e FROM EventEntity e", EventEntity::class.java)
        query.resultList
    } catch (e: Exception) {
        logger.error("Error fetching events: ${e.message}")
        throw e
    }

    override fun deleteEvent(event: EventEntity): Boolean = try {
        entityManager.remove(entityManager.contains(event).let { if (it) event else entityManager.merge(event) })
        entityManager.flush()
        true
    } catch (e: Exception) {
        logger.error("Error deleting event: ${e.message}")
        throw e
    }

    override fun addUserToEvent(event: EventEntity, user: UserEntity): Boolean = try {
        val userEvent = UserEventEntity(user, event)
        entityManager.persist(userEvent)
        entityManager.flush()
        true
    } catch (e: Exception) {
        logger.error("Error adding user to event: ${e.message}")
        throw e
    }

    override fun removeUserFromEvent(event: EventEntity, user: UserEntity): Boolean = try {
        val query = entityManager.createQuery(
            "SELECT ue FROM UserEventEntity ue WHERE ue.user = :user AND ue.event = :event",
            UserEventEntity::class.java
        )
        query.setParameter("user", user)
        query.setParameter("event", event)
        val userEvent = query.singleResult
        entityManager.remove(userEvent)
        entityManager.flush()
        true
    } catch (e: Exception) {
        logger.error("Error removing user from event: ${e.message}")
        throw e
    }

    override fun registerDonation(event: EventEntity, donation: DonationEntity, quantity: Int, actor: UserEntity): Boolean = try {
        val eventDonation = EventDonationEntity(donation, event, quantity)
        entityManager.persist(eventDonation)
        entityManager.flush()
        true
    } catch (e: Exception) {
        logger.error("Error registering donation for event: ${e.message}")
        throw e
    }
}