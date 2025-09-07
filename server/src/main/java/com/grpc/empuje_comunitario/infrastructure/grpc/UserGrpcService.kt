package com.grpc.empuje_comunitario.infrastructure.grpc

import com.grpc.empuje_comunitario.application.user.UserRepositoryImpl
import com.grpc.empuje_comunitario.domain.user.Role
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.proto.CreateUserRequest
import com.grpc.empuje_comunitario.proto.CreateUserResponse
import com.grpc.empuje_comunitario.proto.UserServiceGrpc
import io.grpc.Status
import io.grpc.stub.StreamObserver
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.grpc.server.service.GrpcService
import org.springframework.transaction.annotation.Transactional

@GrpcService
open class UserGrpcService @Autowired constructor(
    private val userRepository: UserRepositoryImpl
) : UserServiceGrpc.UserServiceImplBase() {

    @Transactional
    override fun createUser(
        request: CreateUserRequest,
        responseObserver: StreamObserver<CreateUserResponse>
    ) {
        try {
            val user = User.create(
                request.username,
                request.name,
                request.lastname,
                request.phone,
                request.email,
                Role.valueOf(request.role)
            )
            val result = userRepository.create(user)

            val response = CreateUserResponse.newBuilder()
                .setSuccess(result == com.grpc.empuje_comunitario.domain.Result.SUCCESS)
                .setMessage("User created successfully")
                .build()

            responseObserver.onNext(response)
            responseObserver.onCompleted()
        } catch (e: IllegalArgumentException) {
            System.err.println("Validation error: ${e.message}")
            val status = Status.INVALID_ARGUMENT.withDescription(e.message)
            responseObserver.onError(status.asRuntimeException())
        } catch (e: Exception) {
            System.err.println("Unexpected error: ${e.message}")
            val status = Status.INTERNAL.withDescription("Error: ${e.message}")
            responseObserver.onError(status.asRuntimeException())
        }
    }
}