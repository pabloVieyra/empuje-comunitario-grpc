package com.grpc.empuje_comunitario.application.user;

import com.grpc.empuje_comunitario.domain.Result;
import com.grpc.empuje_comunitario.domain.user.User;
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity;
import org.springframework.stereotype.Repository;

@Repository
public class UserRepositoryImpl implements UserRepository {

    private final PasswordGenerator passwordGenerator;
    private final NetworkDatabase networkDatabase;

    public UserRepositoryImpl(
        PasswordGenerator passwordGenerator,
        NetworkDatabase networkDatabase
    ) {
        this.passwordGenerator = passwordGenerator;
        this.networkDatabase = networkDatabase;
    }

    @Override
    public Result create(User user) {
        String password = passwordGenerator.generateRandomPassword();
        UserEntity userEntity = new UserEntity(
                user.id().value(),
                user.username(),
                user.name(),
                user.lastname(),
                user.phone(),
                user.email(),
                password,
                user.role().name(),
                user.isActive()
        );
        networkDatabase.saveUser(userEntity);
        return Result.SUCCESS;
    }

    @Override
    public boolean existsByUsername(String username) {
        return false;
    }

    @Override
    public boolean existsByEmail(String email) {
        return false;
    }
}
