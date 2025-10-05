package com.grpc.empuje_comunitario.domain.messages

class CancelRequestModel {
    data class CancelRequestModel(
     // ID de la organización solicitante
        val requesterOrgId: String,
        // ID de la solicitud
        val requestId: String
    )
}