package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.UserRepository
import com.grpc.empuje_comunitario.domain.MyResult
import org.springframework.stereotype.Component

@Component
class DisableUserUseCase(
    private val userRepository: UserRepository
) {
    fun invoke(userId: String): MyResult<Unit> {
        val result = userRepository.findUserById(userId)
        if (result !is MyResult.Success) {
            throw Exception("[PERSISTENT]Failed to fetch the user from the repository.")
        }
        result.data.active = false
        val updatedUser = result.data
        return try {
            userRepository.updateUser(updatedUser)
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}