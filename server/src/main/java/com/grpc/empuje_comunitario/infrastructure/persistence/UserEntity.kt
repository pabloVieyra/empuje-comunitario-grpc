package com.grpc.empuje_comunitario.infrastructure.persistence

import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.user.asString
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.Id
import jakarta.persistence.Table

@Entity
@Table(name = "users")
data class UserEntity(
    @Id
    val id: String = "",

    @Column(unique = true, nullable = false)
    val username: String = "",

    @Column(nullable = false)
    val name: String = "",

    @Column(nullable = false)
    val lastname: String = "",

    val phone: String? = null,

    @Column(unique = true, nullable = false)
    val email: String = "",

    @Column(nullable = false)
    val password: String = "",

    @Column(nullable = false)
    val role: String = "",

    @Column(nullable = false)
    val active: Boolean = false
) {
    // No-arg constructor for JPA
    constructor() : this("", "", "", "", null, "", "", "", false)
}

fun User.toUserEntity(password: String): UserEntity {
    return UserEntity(
        id = this.id.value,
        username = this.username,
        name = this.name,
        lastname = this.lastname,
        phone = this.phone,
        email = this.email,
        password = password,
        role = this.role.asString(),
        active = this.active
    )
}