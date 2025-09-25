package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class AddUserToEventUseCase(
    private val eventRepository: EventRepository
) {

    fun invoke(eventId: Int, userId: String, actorId: String): MyResult<Unit> {
        val result = eventRepository.addUser(eventId, userId, actorId)

        return when (result) {
            is MyResult.Success -> MyResult.Success(Unit)
            is MyResult.Failure -> MyResult.Failure(
                Exception("[EVENT] Failed to add user to event: ${result.error}")
            )
        }
    }
}
