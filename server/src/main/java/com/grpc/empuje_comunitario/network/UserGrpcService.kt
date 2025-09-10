package com.grpc.empuje_comunitario.network

import com.grpc.empuje_comunitario.controller.user.UserController
import com.grpc.empuje_comunitario.domain.MyResult
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

        val response = when (result) {
            is MyResult.Success -> GenericResponse.newBuilder()
                .setSuccess(true)
                .setMessage("User created successfully")
                .build()
            is MyResult.Failure -> {
                val errorMessage = when {
                    result.error is IllegalArgumentException -> "Validation error: ${result.error.message}"
                    result.error.message?.contains("duplicate key") == true -> "User already exists"
                    else -> "Error creating user: ${result.error.message ?: "Unknown error"}"
                }
                GenericResponse.newBuilder()
                    .setSuccess(false)
                    .setMessage(errorMessage)
                    .build()
            }
        }

        responseObserver.onNext(response)
        responseObserver.onCompleted()
    }
}