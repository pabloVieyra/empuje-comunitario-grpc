package com.grpc.empuje_comunitario.infrastructure.persistence;

import com.grpc.empuje_comunitario.domain.user.*;
import org.springframework.stereotype.Repository;

import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;

@Repository
public class PostgreSQLUserRepository implements UserRepository {

    @PersistenceContext
    private EntityManager entityManager;

    @Override
    public void save(User user) {
        UserEntity entity = toEntity(user);
        entityManager.persist(entity);
    }

    @Override
    public boolean existsByUsername(Username username) {
        Long count = entityManager.createQuery(
                        "SELECT COUNT(u) FROM UserEntity u WHERE u.username = :username", Long.class)
                .setParameter("username", username.value())
                .getSingleResult();
        return count > 0;
    }

    @Override
    public boolean existsByEmail(Email email) {
        Long count = entityManager.createQuery(
                        "SELECT COUNT(u) FROM UserEntity u WHERE u.email = :email", Long.class)
                .setParameter("email", email.value())
                .getSingleResult();
        return count > 0;
    }

    private UserEntity toEntity(User user) {
        return new UserEntity(
                user.id().value(),
                user.username().value(),
                user.name(),
                user.lastname(),
                user.phone(),
                user.email().value(),
                user.password(),
                user.role().name(),
                user.isActive()
        );
    }
}