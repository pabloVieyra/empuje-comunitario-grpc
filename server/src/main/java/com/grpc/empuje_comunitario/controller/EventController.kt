package com.grpc.empuje_comunitario.controller.event

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import com.grpc.empuje_comunitario.domain.usecases.eventusecase.*
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component
import java.time.LocalDateTime

@Component
class EventController @Autowired constructor(
    private val createEventUseCase: CreateEventUseCase,
    private val listEventUseCase: ListEventUseCase,
    private val addUserToEventUseCase: AddUserToEventUseCase,
    private val removeUserFromEventUseCase: RemoveUserFromEventUseCase,
    private val registerDonationUseCase: RegisterDonationToEventUseCase,
    private val updateEventUseCase: UpdateEventUseCase,
    private val deleteEventUseCase: DeleteEventUseCase
) {

    fun createEvent(eventName: String, description: String, eventDateTime: String, createUser: String): MyResult<Unit> = try {
        val event = Event.create(
            eventName = eventName,
            description = description,
            eventDateTime = LocalDateTime.parse(eventDateTime)
        )
        createEventUseCase.invoke(event, createUser)
        MyResult.Success(Unit)   // ✅ devolvés el
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun listEvents(): MyResult<List<Event>> = try {
        val events = listEventUseCase.invoke()
        MyResult.Success(events)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun addUserToEvent(eventId: Int, userId: String, actorId: String): MyResult<Unit> = try {
        addUserToEventUseCase.invoke(eventId, userId, actorId)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun removeUserFromEvent(eventId: Int, userId: String, actorId: String): MyResult<Unit> = try {
        removeUserFromEventUseCase.invoke(eventId, userId, actorId) // devuelve Unit
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun registerDonationToEvent(eventId: Int, donationId: String, quantity: Int, actorId: String): MyResult<Unit> = try {
        registerDonationUseCase.invoke(eventId, donationId, quantity, actorId)
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun updateEvent(eventId: Int, eventName: String, description: String, eventDateTime: String): MyResult<Unit> = try {
        val event = Event(
            id = eventId,
            eventName = eventName,
            description = description,
            eventDateTime = java.time.LocalDateTime.parse(eventDateTime),
            modificationUser = null,
            modificationDate = null
        )
        updateEventUseCase.invoke(event)
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun deleteEvent(eventId: Int, modificationUserId: String): MyResult<Unit> = try {
        deleteEventUseCase.invoke(eventId, modificationUserId)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }
}
