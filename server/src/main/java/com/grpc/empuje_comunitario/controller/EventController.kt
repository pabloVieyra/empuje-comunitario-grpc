package com.grpc.empuje_comunitario.controller.event

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import com.grpc.empuje_comunitario.domain.usecases.eventusecase.*
import com.grpc.empuje_comunitario.domain.user.Role
import com.grpc.empuje_comunitario.infrastructure.security.JwtTokenGenerator
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component
import java.time.LocalDateTime

@Component
class EventController @Autowired constructor(
    private val createEventUseCase: CreateEventUseCase,
    private val listEventUseCase: ListEventUseCase,
    private val findEventByIdUseCase: FindEventByIdUseCase,
    private val addUserToEventUseCase: AddUserToEventUseCase,
    private val removeUserFromEventUseCase: RemoveUserFromEventUseCase,
    private val registerDonationUseCase: RegisterDonationToEventUseCase,
    private val updateEventUseCase: UpdateEventUseCase,
    private val deleteEventUseCase: DeleteEventUseCase,
    private val jwtTokenGenerator: JwtTokenGenerator
) {

    fun createEvent(eventName: String, description: String, eventDateTime: String, createUser: String, token: String): MyResult<Unit> = try {
        validatePresidentToken(token)
        val event = Event.create(
            eventName = eventName,
            description = description,
            eventDateTime = LocalDateTime.parse(eventDateTime),
        )
        createEventUseCase.invoke(event, createUser)
        MyResult.Success(Unit)   // ✅ devolvés el
    } catch (e: Exception) {
        MyResult.Failure(e)
    }
    fun findEventById(eventId: Int, token :String): MyResult<Event> = try {
        validatePresidentToken(token)
        val event = findEventByIdUseCase.invoke(eventId)
        MyResult.Success(event)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }
    fun listEvents(token:String): MyResult<List<Event>> = try {
        validatePresidentToken(token)
        val events = listEventUseCase.invoke()
        MyResult.Success(events)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun addUserToEvent(eventId: Int, userId: String, actorId: String, token:String): MyResult<Unit> = try {
        validatePresidentToken(token)
        addUserToEventUseCase.invoke(eventId, userId, actorId)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun removeUserFromEvent(eventId: Int, userId: String, actorId: String, token:String): MyResult<Unit> = try {
        validatePresidentToken(token)
        removeUserFromEventUseCase.invoke(eventId, userId, actorId) // devuelve Unit
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun registerDonationToEvent(eventId: Int, donationId: String, quantity: Int, actorId: String, token: String): MyResult<Unit> = try {
        validatePresidentToken(token)
        registerDonationUseCase.invoke(eventId, donationId, quantity, actorId)
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun updateEvent(eventId: Int, eventName: String, description: String, eventDateTime: String, actorId: String, token: String): MyResult<Unit> = try {
        validatePresidentToken(token)
        val event = Event(
            id = eventId,
            eventName = eventName,
            description = description,
            eventDateTime = java.time.LocalDateTime.parse(eventDateTime),
            modificationUser = actorId,
            modificationDate = null
        )
        updateEventUseCase.invoke(event)
        MyResult.Success(Unit)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    fun deleteEvent(eventId: Int, modificationUserId: String, token : String): MyResult<Unit> = try {
        validatePresidentToken(token)
        deleteEventUseCase.invoke(eventId, modificationUserId)
    } catch (e: Exception) {
        MyResult.Failure(e)
    }

    private fun validatePresidentToken(token: String) {
        val result = jwtTokenGenerator.validateAndGetSubjectAndRole(token)
        if (!result.valid) throw IllegalArgumentException("Token inválido o expirado")
        if (result.role != Role.PRESIDENT.toString()) {
            throw IllegalAccessException("No autorizado para listar usuarios")
        }
    }
}
