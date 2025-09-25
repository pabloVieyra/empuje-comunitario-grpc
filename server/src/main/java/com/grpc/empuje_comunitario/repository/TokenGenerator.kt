package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.user.User

interface TokenGenerator {
    fun generateToken(user: User): String
}