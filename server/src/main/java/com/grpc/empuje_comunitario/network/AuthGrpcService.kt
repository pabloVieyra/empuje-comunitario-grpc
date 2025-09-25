package com.grpc.empuje_comunitario.network

import com.grpc.empuje_comunitario.domain.AuthController
import com.grpc.empuje_comunitario.controller.AuthControllerImpl
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.proto.LoginRequest
import com.grpc.empuje_comunitario.proto.LoginResponse
import com.grpc.empuje_comunitario.proto.AuthServiceGrpc
import io.grpc.stub.StreamObserver
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.beans.factory.annotation.Qualifier
import org.springframework.grpc.server.service.GrpcService
import org.springframework.transaction.annotation.Transactional


@GrpcService
open class AuthGrpcService @Autowired constructor(
    @Qualifier("AuthControllerImpl") private val authController: AuthController
) : AuthServiceGrpc.AuthServiceImplBase() {

//    override fun myLogin() {
//        this.login(
//            ...
//        )
//    }

    @Transactional
    override fun login(
        request: LoginRequest,
        responseObserver: StreamObserver<LoginResponse>
    ) {
        val result = authController.onLogin(request.usernameOrEmail, request.password)
        val response = when (result) {
            is MyResult.Success -> LoginResponse.newBuilder()
                .setSuccess(true)
                .setToken(result.data.first)
                .setRole(result.data.second)
                .setMessage("Authentication successful").build()

            is MyResult.Failure -> LoginResponse.newBuilder()
                .setSuccess(false)
                .setMessage(result.error.message ?: "Error de autenticación")
                .build()
        }
        responseObserver.onNext(response)
        responseObserver.onCompleted()
    }
}