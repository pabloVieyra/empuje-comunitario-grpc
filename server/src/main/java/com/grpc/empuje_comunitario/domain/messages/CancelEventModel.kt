package com.grpc.empuje_comunitario.domain.messages

data class CancelEventModel(
    // ID de la organización
    val orgId: String,
    // ID del evento
    val eventId: String
)