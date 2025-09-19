package com.grpc.empuje_comunitario.controller

import com.grpc.empuje_comunitario.domain.usecases.LoginUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class AuthController(
    private val loginUserUseCase: LoginUserUseCase
) {
    fun login(email: String, password: String): MyResult<Pair<String, String>> {
        return try {
            val response = loginUserUseCase.invoke(email, password)
            MyResult.Success(response)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}