package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import org.springframework.stereotype.Component



@Component
class ListEventUseCase(
    private val eventRepository: EventRepository
) {
    fun invoke(): List<Event> {
        val result = eventRepository.findAll()
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to fetch users from the repository.")
        }
        return result.data
    }
}