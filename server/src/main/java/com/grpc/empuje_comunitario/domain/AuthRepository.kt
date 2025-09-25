package com.grpc.empuje_comunitario.domain

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.user.User

interface AuthRepository {
    fun findUserByEmail(email: String): MyResult<User>
    fun checkPassword(email: String, password: String): MyResult<Unit>
    fun generateToken(user: User): String
}