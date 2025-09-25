package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import com.grpc.empuje_comunitario.domain.donation.toEventEntity
import org.springframework.stereotype.Component

@Component
class CreateEventUseCase(
    private val eventRepository: EventRepository,
) {
    operator fun invoke(event: Event, createUser: String) {

        val eventResult = eventRepository.create(event)
        if (eventResult !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to persist the new user in the repository.")
        }
    }
}
