package com.grpc.empuje_comunitario.domain.messages

data class DonationItemModel(

    val category: String,

    val description: String,

    val quantity: Int? = null
)