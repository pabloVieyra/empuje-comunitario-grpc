package com.grpc.empuje_comunitario.domain.user

import java.util.UUID

data class UserId  constructor(val value: String) {
    companion object {
        fun newId(): UserId {
            return UserId(UUID.randomUUID().toString())
        }

        fun from(value: String): UserId {
            return UserId(value)
        }
    }
}