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
            return
        }
        val userRepositoryResult = createUserRepository.createWithPassword(user, result.data.encrypted)
        if (userRepositoryResult !is MyResult.Success) {
            return
        }
        val sendEmailResult = sendEmailUseCase(
            user.email,
            "Welcome to Empuje Comunitario",
            "Your password is: ${result.data.plain}"
        )
        if (sendEmailResult !is MyResult.Success) {
            return
        }
    }
}
