package com.grpc.empuje_comunitario.domain.messages

class OfferDonationModel {
    data class OfferDonationModel(
        // ID de oferta
        val offerId: String,
        // ID de la organización donante
        val donorOrgId: String,
        // Lista de donaciones ofrecidas (con cantidad)
        val donations: List<DonationItemModel>
    )
}