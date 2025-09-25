package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import org.springframework.stereotype.Component

@Component
class FindEventByIdUseCase(
    private val eventRepository: EventRepository
) {
    fun invoke(eventId: Int): Event {
        val result = eventRepository.findById(eventId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENT] Failed to fetch event with id=$eventId from the repository.")
        }
        return result.data
    }
}
