package com.grpc.empuje_comunitario.domain.messages

import java.time.LocalDateTime

data class SolidaryEventModel(
    // ID de la organización
    val orgId: String,
    // ID del evento
    val eventId: String,
    // Nombre del evento
    val name: String,
    // Descripción
    val description: String,
    // Fecha y hora
    val dateTime: LocalDateTime
)