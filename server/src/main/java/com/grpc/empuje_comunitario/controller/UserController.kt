package com.grpc.empuje_comunitario.controller.user

import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.usecases.CreateUserUseCase
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.usecases.DisableUserUseCase
import com.grpc.empuje_comunitario.domain.usecases.ListUsersUseCase
import com.grpc.empuje_comunitario.domain.usecases.UpdateUserUseCase
import com.grpc.empuje_comunitario.domain.user.Role
import com.grpc.empuje_comunitario.domain.user.UserId
import com.grpc.empuje_comunitario.domain.user.toRole
import com.grpc.empuje_comunitario.infrastructure.security.JwtTokenGenerator
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Component

@Component
class UserController @Autowired constructor(
    private val createUserUseCase: CreateUserUseCase,
    private val listUsersUseCase: ListUsersUseCase,
    private val updateUserUseCase: UpdateUserUseCase,
    private val disableUserUseCase: DisableUserUseCase,
    private val jwtTokenGenerator: JwtTokenGenerator
) {
    fun createUser(
        username: String,
        name: String,
        lastname: String,
        phone: String,
        email: String,
        role: String,
        token: String
    ): MyResult<Unit> {
        return try {
            validatePresidentToken(token)
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
            validatePresidentToken(token)
            val users = listUsersUseCase.invoke()
            MyResult.Success(users)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    fun updateUser(
        id: String,
        username: String,
        name: String,
        lastname: String,
        phone: String,
        email: String,
        role: String,
        active: Boolean,
        token: String
    ): MyResult<User> {
        return try {
            validatePresidentToken(token)
            val user = User(
                id = UserId.from(id),
                username = username,
                name = name,
                lastname = lastname,
                phone = phone,
                email = email,
                role = role.toRole(),
                active = active
            )
            updateUserUseCase.invoke(user)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    fun disableUser(
        userId: String,
        token: String
    ): MyResult<Unit> {
        return try {
            validatePresidentToken(token)
            disableUserUseCase.invoke(userId)
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    private fun validatePresidentToken(token: String) {
        val result = jwtTokenGenerator.validateAndGetSubjectAndRole(token)
        if (!result.valid) throw IllegalArgumentException("Token inválido o expirado")
        if (result.role != Role.PRESIDENT.toString()) {
            throw IllegalAccessException("No autorizado para listar usuarios")
        }
    }
}