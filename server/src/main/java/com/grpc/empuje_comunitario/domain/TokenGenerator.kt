package com.grpc.empuje_comunitario.domain

import com.grpc.empuje_comunitario.domain.user.User

interface TokenGenerator {
    fun generateToken(user: User): String
}