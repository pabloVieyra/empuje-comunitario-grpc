package com.grpc.empuje_comunitario.application.user;

import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity;
import org.springframework.stereotype.Component;

@Component
public interface NetworkDatabase {
    boolean saveUser(UserEntity user);
}
