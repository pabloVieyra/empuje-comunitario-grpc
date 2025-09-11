package com.grpc.empuje_comunitario.domain.usecases;

import com.grpc.empuje_comunitario.repository.PasswordGenerator
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.UserRepository
import com.grpc.empuje_comunitario.domain.user.User
import org.springframework.stereotype.Component

@Component
class CreateUserUseCase(
    private val passwordGenerator: PasswordGenerator,
    private val createUserRepository: UserRepository,
    private val sendEmailUseCase: SendEmailUseCase
) {
    operator fun invoke(user: User) {
        val result = passwordGenerator.generateRandomPassword()
        if (result !is MyResult.Success) {
            throw Exception("[PASSWORD]Failed to generate user password. Please try again later.")
        }
        val userRepositoryResult = createUserRepository.createWithPassword(user, result.data.encrypted)
        if (userRepositoryResult !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to persist the new user in the repository.")
        }
        val sendEmailResult = sendEmailUseCase(
            user.email,
            "Welcome to Empuje Comunitario",
            "Your password is: ${result.data.plain}"
        )
        if (sendEmailResult !is MyResult.Success) {
            throw Exception("[MAIL]Failed to send welcome email to the user.")
        }
    }
}
