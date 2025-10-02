package com.grpc.empuje_comunitario.domain.donation

class EventDonation(
    val donation: Donation,
    val event :Event,
    val quantity: Int
)