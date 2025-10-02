package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import org.springframework.stereotype.Component


@Component
class UpdateEventUseCase(
    private val eventRepository: EventRepository
) {
    operator fun invoke(event: Event): MyResult<Event> {
        return try {
            val result = eventRepository.update(event)
            if (result is MyResult.Success) {
                MyResult.Success(result.data)
            } else {
                MyResult.Failure(Exception("[PERSISTENT]Failed to update the user in the repository."))
            }
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}