package com.grpc.empuje_comunitario.domain.usecases.eventusecase

import com.grpc.empuje_comunitario.domain.EventRepository
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class RegisterDonationToEventUseCase(
    private val eventRepository: EventRepository
) {
    fun invoke(eventId: Int, donationId: String, quantity: Int, actorId: String) {
        val result = eventRepository.registerDonation(eventId, donationId, quantity, actorId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENCE] Failed to register donation $donationId (qty=$quantity) in event $eventId by actor $actorId.")
        }
    }
}
