package com.grpc.empuje_comunitario.network.grpc

import com.grpc.empuje_comunitario.controller.event.EventController
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Event
import com.grpc.empuje_comunitario.proto.*
import io.grpc.stub.StreamObserver
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.grpc.server.service.GrpcService
import org.springframework.transaction.annotation.Transactional
/*
@GrpcService
open class EventGrpcService @Autowired constructor(
    private val eventController: EventController
) : EventServiceGrpc.EventServiceImplBase() {

    @Transactional
    override fun createEvent(
        request: CreateEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.createEvent(
            eventName = request.eventName,
            description = request.description,
            eventDateTime = request.eventDateTime
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun listEvents(
        request: ListEventsRequest,
        responseObserver: StreamObserver<ListEventsResponse>
    ) {
        val result = eventController.listEvents()

        when (result) {
            is MyResult.Success -> {
                val response = ListEventsResponse.newBuilder()
                    .setSuccess(true)
                    .setMessage("Events fetched successfully")
                    .addAllEvents(result.data.map { it.toProto() })
                    .build()
                responseObserver.onNext(response)
            }
            is MyResult.Failure -> {
                val response = ListEventsResponse.newBuilder()
                    .setSuccess(false)
                    .setMessage(mapErrorMessage(result.error))
                    .build()
                responseObserver.onNext(response)
            }
        }
        responseObserver.onCompleted()
    }

    @Transactional
    override fun updateEvent(
        request: UpdateEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.updateEvent(
            eventId = request.eventId,
            eventName = request.eventName,
            description = request.description,
            eventDateTime = request.eventDateTime
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun deleteEvent(
        request: DeleteEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.deleteEvent(
            eventId = request.eventId,
            modificationUserId = request.actorId
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun addUserToEvent(
        request: AddUserToEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.addUserToEvent(
            eventId = request.eventId,
            userId = request.userId,
            actorId = request.actorId
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun removeUserFromEvent(
        request: RemoveUserFromEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.removeUserFromEvent(
            eventId = request.eventId,
            userId = request.userId,
            actorId = request.actorId
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun registerDonationToEvent(
        request: RegisterDonationToEventRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = eventController.registerDonationToEvent(
            eventId = request.eventId,
            donationId = request.donationId,
            quantity = request.quantity,
            actorId = request.actorId
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    private fun MyResult<Unit>.toGenericResponse(): GenericResponse {
        return when (this) {
            is MyResult.Success -> GenericResponse.newBuilder()
                .setSuccess(true)
                .setMessage("Operation successful")
                .build()
            is MyResult.Failure -> GenericResponse.newBuilder()
                .setSuccess(false)
                .setMessage(mapErrorMessage(this.error))
                .build()
        }
    }

    private fun mapErrorMessage(error: Throwable): String =
        when {
            error is IllegalArgumentException -> "Validation error: ${error.message}"
            else -> "Error: ${error.message ?: "Unknown error"}"
        }
}

private fun Event.toProto(): EventProto {
    return EventProto.newBuilder()
        .setId(this.id)
        .setEventName(this.eventName)
        .setDescription(this.description)
        .setEventDateTime(this.eventDateTime.toString())
        .build()
}
*/