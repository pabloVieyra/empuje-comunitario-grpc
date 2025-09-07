package com.grpc.empuje_comunitario.application.user;

import com.grpc.empuje_comunitario.domain.Result;
import com.grpc.empuje_comunitario.domain.user.User;

public interface UserRepository {
    Result create(User user);
}
