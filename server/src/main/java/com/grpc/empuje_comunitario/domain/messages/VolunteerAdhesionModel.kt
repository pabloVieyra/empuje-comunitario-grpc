package com.grpc.empuje_comunitario.domain.messages

data class VolunteerAdhesionModel(
    // ID del evento al que se adhiere
    val eventId: String,
    // ID Organización (la tuya)
    val orgId: String,
    // ID Voluntario
    val volunteerId: String,
    // Nombre del voluntario
    val name: String,
    // Apellido del voluntario
    val lastName: String,
    // Teléfono
    val phone: String? = null,
    //Email
    val email: String
)
