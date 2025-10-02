package com.grpc.empuje_comunitario.domain.usecases.eventusecase


import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class RemoveUserFromEventUseCase(
    private val eventRepository: EventRepository
) {
    fun invoke(eventId: Int, userId: String, actorId: String) {
        val result = eventRepository.removeUser(eventId, userId, actorId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENCE] Failed to remove user $userId from event $eventId by actor $actorId.")
        }
    }
}
