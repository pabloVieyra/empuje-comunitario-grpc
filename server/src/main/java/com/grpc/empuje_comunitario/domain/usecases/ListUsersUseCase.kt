package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.UserRepository
import com.grpc.empuje_comunitario.domain.user.User
import org.springframework.stereotype.Component

@Component
class ListUsersUseCase(
    private val userRepository: UserRepository
) {
    fun invoke(): List<User> {
        val result = userRepository.findAll()
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTANT]Failed to fetch users from the repository.")
        }
        return result.data
    }
}