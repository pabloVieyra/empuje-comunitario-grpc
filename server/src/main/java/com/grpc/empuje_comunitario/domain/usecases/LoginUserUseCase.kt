package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.AuthRepository
import com.grpc.empuje_comunitario.domain.TokenGenerator
import org.springframework.stereotype.Component

@Component
class LoginUserUseCase(
    private val authRepository: AuthRepository,
    private val tokenGenerator: TokenGenerator
) {
     fun invoke(email: String, password: String): String {
        //TODO: pendiente hacerlo tambien por username
        val userResult = authRepository.findUserByEmail(email)
        if (userResult !is MyResult.Success) {
            throw Exception("[AUTH] User or email does not exist.")
        }
        val user = userResult.data

        val passwordResult = authRepository.checkPassword(user.email, password)
        if (passwordResult !is MyResult.Success) {
            throw Exception("[AUTH] Incorrect credentials.")
        }

        val token = tokenGenerator.generateToken(user)
        if (token.isEmpty()) {
            throw Exception("[TOKEN] Failed to generate token.")
        }
        return token
    }
}