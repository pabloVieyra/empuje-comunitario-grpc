package com.grpc.empuje_comunitario.controller.user

import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.usecases.CreateUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.user.toRole
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component

@Component
class UserController @Autowired constructor(
    private val createUserUseCase: CreateUserUseCase
) {
    fun createUser(
        username: String,
        name: String,
        lastname: String,
        phone: String,
        email: String,
        role: String
    ): MyResult<Unit> {
        return try {
            val user = User.create(
                username = username,
                name = name,
                lastname = lastname,
                phone = phone,
                email = email,
                role = role.toRole()
            )
            createUserUseCase(user)
            MyResult.Success(Unit)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}