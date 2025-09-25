package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.AuthRepository
import com.grpc.empuje_comunitario.repository.TokenGenerator
import com.grpc.empuje_comunitario.domain.user.asString
import org.springframework.stereotype.Component

@Component
class LoginUserUseCase(
    private val authRepository: AuthRepository,
) {
     fun invoke(email: String, password: String): Pair<String, String> {
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

         val token = authRepository.generateToken(user)
        //val token = tokenGenerator.generateToken(user)
        if (token.isEmpty()) {
            throw Exception("[TOKEN] Failed to generate token.")
        }
        return Pair(token, user.role.asString())
    }
}