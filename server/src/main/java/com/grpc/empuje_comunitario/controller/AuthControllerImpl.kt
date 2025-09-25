package com.grpc.empuje_comunitario.controller

import com.grpc.empuje_comunitario.domain.AuthController
import com.grpc.empuje_comunitario.domain.usecases.LoginUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult

import org.springframework.stereotype.Service

//Falta interface en Domain
@Service("AuthControllerImpl")
class AuthControllerImpl(
    private val loginUserUseCase: LoginUserUseCase
): AuthController {
    override fun onLogin(
        email: String,
        password: String
    ): MyResult<String> {
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