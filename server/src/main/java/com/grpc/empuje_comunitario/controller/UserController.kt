package com.grpc.empuje_comunitario.controller.user

import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.usecases.CreateUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.usecases.ListUsersUseCase
import com.grpc.empuje_comunitario.domain.user.Role
import com.grpc.empuje_comunitario.domain.user.toRole
import com.grpc.empuje_comunitario.infrastructure.security.JwtTokenGenerator
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component

@Component
class UserController @Autowired constructor(
    private val createUserUseCase: CreateUserUseCase,
    private val listUsersUseCase: ListUsersUseCase,
    private val jwtTokenGenerator: JwtTokenGenerator
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

    fun listUsers(token: String): MyResult<List<User>> {
        return try {
            val user = jwtTokenGenerator.validateAndGetSubjectAndRole(token)
            if(!user.first) throw IllegalArgumentException("Token inválido o expirado")
            if (user.second != Role.PRESIDENT.toString()) {
                throw IllegalAccessException("No autorizado para listar usuarios")
            }
            val users = listUsersUseCase.invoke()
            MyResult.Success(users)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

}