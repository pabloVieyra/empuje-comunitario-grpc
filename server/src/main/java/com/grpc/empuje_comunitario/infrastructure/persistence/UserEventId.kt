package com.grpc.empuje_comunitario.infrastructure.persistence

import java.io.Serializable

data class UserEventId(
    val user: String = "",  // coincide con UserEntity.id
    val event: Int = 0      // coincide con EventEntity.id
) : Serializable