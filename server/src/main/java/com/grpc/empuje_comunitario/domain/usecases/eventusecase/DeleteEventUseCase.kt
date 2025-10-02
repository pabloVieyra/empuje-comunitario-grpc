package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class DeleteEventUseCase(
    private val eventRepository: EventRepository
) {

    fun invoke(eventId: Int, modificationUserId: String): MyResult<Unit> {
        val result = eventRepository.delete(eventId, modificationUserId)

        return when (result) {
            is MyResult.Success -> MyResult.Success(Unit)
            is MyResult.Failure -> MyResult.Failure(
                Exception("[EVENT] Failed to delete event: ${result.error}")
            )
        }
    }
}
