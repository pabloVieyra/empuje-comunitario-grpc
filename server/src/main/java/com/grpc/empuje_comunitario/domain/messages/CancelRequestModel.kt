package com.grpc.empuje_comunitario.domain.messages

class CancelRequestModel (

        val requesterOrgId: String,
        // ID de la solicitud
        val requestId: String

    )