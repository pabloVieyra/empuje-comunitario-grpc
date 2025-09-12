package com.grpc.empuje_comunitario.domain.usecases

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.UserRepository
import com.grpc.empuje_comunitario.domain.user.User
import org.springframework.stereotype.Component

@Component
class UpdateUserUseCase(
    private val userRepository: UserRepository
) {
    operator fun invoke(user: User): MyResult<User> {
        return try {
            val result = userRepository.updateUser(user)
            if (result is MyResult.Success) {
                MyResult.Success(result.data)
            } else {
                MyResult.Failure(Exception("[PERSISTENT]Failed to update the user in the repository."))
            }
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}