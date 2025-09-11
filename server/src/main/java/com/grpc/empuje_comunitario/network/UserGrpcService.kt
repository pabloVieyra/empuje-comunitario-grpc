package com.grpc.empuje_comunitario.network.grpc

import com.grpc.empuje_comunitario.controller.user.UserController
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.proto.*
import io.grpc.stub.StreamObserver
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.grpc.server.service.GrpcService
import org.springframework.transaction.annotation.Transactional

@GrpcService
open class UserGrpcService @Autowired constructor(
    private val userController: UserController
) : UserServiceGrpc.UserServiceImplBase() {

    @Transactional
    override fun createUser(
        request: CreateUserRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = userController.createUser(
            username = request.username,
            name = request.name,
            lastname = request.lastname,
            phone = request.phone,
            email = request.email,
            role = request.role
        )

        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun listUsers(
        request: ListUsersRequest,
        responseObserver: StreamObserver<ListUsersResponse>
    ) {
        val apiToken = request.token
        val usersResult = userController.listUsers(apiToken)

        when (usersResult) {
            is MyResult.Success -> {
                val response = ListUsersResponse.newBuilder()
                    .setSuccess(true)
                    .setMessage("Users fetched successfully")
                    .addAllUsers(usersResult.data.map { it.toProto() })
                    .build()
                responseObserver.onNext(response)
            }
            is MyResult.Failure -> {
                val response = ListUsersResponse.newBuilder()
                    .setSuccess(false)
                    .setMessage(mapErrorMessage(usersResult.error))
                    .build()
                responseObserver.onNext(response)
            }
        }
        responseObserver.onCompleted()
    }

    private fun MyResult<Unit>.toGenericResponse(): GenericResponse {
        return when (this) {
            is MyResult.Success -> GenericResponse.newBuilder()
                .setSuccess(true)
                .setMessage("User created successfully")
                .build()
            is MyResult.Failure -> GenericResponse.newBuilder()
                .setSuccess(false)
                .setMessage(mapErrorMessage(this.error))
                .build()
        }
    }

    private fun mapErrorMessage(error: Throwable): String =
        when {
            error is IllegalArgumentException -> "Validation error: ${error.message}"
            error.message?.contains("duplicate key") == true -> "User already exists"
            else -> "Error creating user: ${error.message ?: "Unknown error"}"
        }
}

private fun User.toProto(): UserProto? {
    return UserProto.newBuilder()
        .setId(this.id.toString())
        .setUsername(this.username)
        .setName(this.name)
        .setLastname(this.lastname)
        .setPhone(this.phone)
        .setEmail(this.email)
        .setRole(this.role.toString())
        .build()
}
