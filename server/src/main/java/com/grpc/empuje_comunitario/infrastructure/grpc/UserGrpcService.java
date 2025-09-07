package com.grpc.empuje_comunitario.infrastructure.grpc;

import com.grpc.empuje_comunitario.application.user.UserRepositoryImpl;
import com.grpc.empuje_comunitario.domain.user.Role;
import com.grpc.empuje_comunitario.domain.user.User;
import com.grpc.empuje_comunitario.proto.CreateUserRequest;
import com.grpc.empuje_comunitario.proto.CreateUserResponse;
import com.grpc.empuje_comunitario.proto.UserServiceGrpc;
import io.grpc.Status;
import io.grpc.stub.StreamObserver;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.grpc.server.service.GrpcService;
import org.springframework.transaction.annotation.Transactional;


@GrpcService
public class UserGrpcService extends UserServiceGrpc.UserServiceImplBase {

    private final UserRepositoryImpl userRepository;

    @Autowired
    public UserGrpcService(UserRepositoryImpl userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    @Transactional
    public void createUser(CreateUserRequest request, StreamObserver<CreateUserResponse> responseObserver) {
        try {
            User user =  User.create(
                    request.getUsername(),
                    request.getName(),
                    request.getLastname(),
                    request.getPhone(),
                    request.getEmail(),
                    Role.valueOf(request.getRole())
            );
            userRepository.create(user);

            CreateUserResponse response = CreateUserResponse.newBuilder()
                    .setSuccess(true)
                    .setMessage("User created successfully")
                    .build();

            responseObserver.onNext(response);
            responseObserver.onCompleted();

        } catch (IllegalArgumentException e) {
            System.err.println("Validation error: " + e.getMessage());
            Status status = Status.INVALID_ARGUMENT.withDescription(e.getMessage());
            responseObserver.onError(status.asRuntimeException());
        } catch (Exception e) {
            System.err.println("Unexpected error: " + e.getMessage());
            Status status = Status.INTERNAL.withDescription("Error: " + e.getMessage());
            responseObserver.onError(status.asRuntimeException());
        }
    }
}