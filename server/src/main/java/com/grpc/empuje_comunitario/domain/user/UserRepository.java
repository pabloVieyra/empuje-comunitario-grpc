package com.grpc.empuje_comunitario.domain.user;

public interface UserRepository {
    void save(User user);
    boolean existsByUsername(Username username);
    boolean existsByEmail(Email email);
}