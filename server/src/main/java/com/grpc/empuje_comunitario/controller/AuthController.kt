package com.grpc.empuje_comunitario.controller.auth

import com.grpc.empuje_comunitario.domain.usecases.LoginUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class AuthController(
    private val loginUserUseCase: LoginUserUseCase
) {
    fun login(email: String, password: String): MyResult<String> {
        return loginUserUseCase(email, password)
    }
}