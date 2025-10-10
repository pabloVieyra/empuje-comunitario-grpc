package com.grpc.empuje_comunitario.domain.messages

data class TransferDonationModel(
    // ID de solicitud (a la que se responde)
    val requestId: String,
    // ID de la organización donante
    val donationOrgId: String,
    val donations: List<DonationItemModel>
)