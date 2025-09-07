package com.grpc.empuje_comunitario.infrastructure.persistence;

import com.grpc.empuje_comunitario.application.user.NetworkDatabase;
import com.grpc.empuje_comunitario.domain.user.*;
import org.springframework.stereotype.Repository;

import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import org.springframework.stereotype.Service;

@Service
public class PostgreSQLUserRepository implements NetworkDatabase {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public boolean saveUser(UserEntity userEntity) {
        try {
            entityManager.persist(userEntity);
            return true;
        } catch (Exception e) {
            return false;
        }
    }

//    @Override
//    public boolean existsByUsername(Username username) {
//        Long count = entityManager.createQuery(
//                        "SELECT COUNT(u) FROM UserEntity u WHERE u.username = :username", Long.class)
//                .setParameter("username", username.value())
//                .getSingleResult();
//        return count > 0;
//    }
//
//    @Override
//    public boolean existsByEmail(Email email) {
//        Long count = entityManager.createQuery(
//                        "SELECT COUNT(u) FROM UserEntity u WHERE u.email = :email", Long.class)
//                .setParameter("email", email.value())
//                .getSingleResult();
//        return count > 0;
//    }

    private UserEntity toEntity(User user, String password) {
        return new UserEntity(
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
    }
}