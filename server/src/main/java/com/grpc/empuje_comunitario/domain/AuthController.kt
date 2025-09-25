package com.grpc.empuje_comunitario.domain

interface AuthController {
    fun onLogin(email: String, password: String): MyResult<String>
}