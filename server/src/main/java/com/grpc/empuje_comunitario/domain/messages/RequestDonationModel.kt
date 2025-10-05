package com.grpc.empuje_comunitario.domain.messages

data class RequestDonationModel(
    val requesterOrgId: String,
    val requestId: String,
    val donations: List<DonationItemModel>
)