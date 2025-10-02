package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.EventRepository
import org.springframework.stereotype.Repository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import com.grpc.empuje_comunitario.domain.donation.toEventEntity
import com.grpc.empuje_comunitario.domain.user.Role
import com.grpc.empuje_comunitario.domain.user.asString
import com.grpc.empuje_comunitario.domain.user.toRole
import com.grpc.empuje_comunitario.infrastructure.persistence.*
import java.time.LocalDateTime

@Repository
open class EventRepositoryImpl(
    private val eventNetworkDatabase: EventNetworkDatabase,
    private val userNetworkDatabase: NetworkDatabase,
    private val donationNetworkDatabase: DonationNetworkDatabase
) : EventRepository {

    override fun create(event: Event): MyResult<Unit> = try {
        require(event.eventDateTime.isAfter(LocalDateTime.now())) { "La fecha del evento debe ser futura" }


        val success = eventNetworkDatabase.saveEvent(event.toEventEntity(null))
        if (success) MyResult.Success(Unit)
        else MyResult.Failure(Exception("Failed to save event"))

    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun update(event: Event): MyResult<Event> = try {
        val eventEntity = eventNetworkDatabase.findById(event.id)
            ?: return MyResult.Failure(Exception("Event not found"))

        val modificationUser = userNetworkDatabase.findUserById(event.modificationUser ?: "")
            ?: return MyResult.Failure(Exception("Modification user not found"))


        if (event.eventDateTime.isAfter(LocalDateTime.now())) {
            eventEntity.eventName = event.eventName
            eventEntity.description = event.description
            eventEntity.eventDateTime = event.eventDateTime
            eventEntity.modificationUser = modificationUser
            eventEntity.modificationDate = event.modificationDate
        }

        val updated = eventNetworkDatabase.updateEvent(eventEntity)
        MyResult.Success(updated.toEvent())

    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun delete(eventId: Int, modificationUserId: String): MyResult<Unit> = try {
        val eventEntity = eventNetworkDatabase.findById(eventId)
            ?: return MyResult.Failure(Exception("Event not found"))
        require(eventEntity.eventDateTime.isAfter(LocalDateTime.now())) { "Solo se pueden borrar eventos a futuro" }

        eventNetworkDatabase.deleteEvent(eventEntity)
        MyResult.Success(Unit)

    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun findById(eventId: Int): MyResult<Event> = try {
        val eventEntity = eventNetworkDatabase.findById(eventId)
            ?: return MyResult.Failure(Exception("Event not found"))
        MyResult.Success(eventEntity.toEvent())
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun findAll(): MyResult<List<Event>> = try {
        val events = eventNetworkDatabase.findAll().map { it.toEvent() }
        MyResult.Success(events)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun addUser(eventId: Int, userId: String, actorId: String): MyResult<Unit> = try {
        val event = eventNetworkDatabase.findById(eventId)
            ?: return MyResult.Failure(Exception("Event not found"))
        val user = userNetworkDatabase.findUserById(userId)
            ?: return MyResult.Failure(Exception("User not found"))
        val actor = userNetworkDatabase.findUserById(actorId)
            ?: return MyResult.Failure(Exception("Actor not found"))



        val success = eventNetworkDatabase.addUserToEvent(event, user)
        if (success) MyResult.Success(Unit)
        else MyResult.Failure(Exception("Failed to add user to event"))

    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    override fun removeUser(eventId: Int, userId: String, actorId: String): MyResult<Unit> {
        return try {
            val event = eventNetworkDatabase.findById(eventId)
                ?: return MyResult.Failure(Exception("Event not found"))

            val user = userNetworkDatabase.findUserById(userId)
                ?: return MyResult.Failure(Exception("User not found"))

            val actor = userNetworkDatabase.findUserById(actorId)
                ?: return MyResult.Failure(Exception("Actor not found"))

            if (event.eventDateTime.isBefore(java.time.LocalDateTime.now())) {
                return MyResult.Failure(Exception("No se puede modificar la lista de miembros de un evento pasado"))
            }

            val actorRole = try {
                actor.role.toRole()
            } catch (e: Exception) {
                return MyResult.Failure(Exception("Rol del actor inválido: ${actor.role}"))
            }


            when (actorRole) {
                Role.PRESIDENT, Role.COORDINATOR -> {
                }
                Role.VOLUNTEER -> {
                    if (actor.id != user.id) {
                        return MyResult.Failure(Exception("Voluntario solo puede quitarse a sí mismo"))
                    }
                }
                else -> {
                    return MyResult.Failure(Exception("Rol no autorizado para quitar miembros"))
                }
            }
            val success = eventNetworkDatabase.removeUserFromEvent(event, user)
            if (success) MyResult.Success(Unit)
            else MyResult.Failure(Exception("Fallo al quitar el usuario del evento"))

        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }


    override fun registerDonation(eventId: Int, donationId: String, quantity: Int, actorId: String): MyResult<Unit> = try {
        val event = eventNetworkDatabase.findById(eventId)
            ?: return MyResult.Failure(Exception("Event not found"))
        val donation = donationNetworkDatabase.findById(donationId)
            ?: return MyResult.Failure(Exception("Donation not found"))
        val actor = userNetworkDatabase.findUserById(actorId)
            ?: return MyResult.Failure(Exception("Actor not found"))

        require(event.eventDateTime.isBefore(LocalDateTime.now())) { "Solo se pueden registrar donaciones en eventos pasados" }

        val success = eventNetworkDatabase.registerDonation(event, donation, quantity, actor)
        if (success) {
            donation.quantity -= quantity
            donationNetworkDatabase.updateDonation(donation)
            MyResult.Success(Unit)
        } else MyResult.Failure(Exception("Failed to register donation"))

    } catch (e: Exception) {
        MyResult.Failure(e)
    }
}