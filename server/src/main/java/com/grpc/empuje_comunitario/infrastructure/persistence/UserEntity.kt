package com.grpc.empuje_comunitario.infrastructure.persistence

    import jakarta.persistence.Column
    import jakarta.persistence.Entity
    import jakarta.persistence.Id
    import jakarta.persistence.Table

    @Entity
    @Table(name = "users")
    data class UserEntity(
        @Id
        val id: String,

        @Column(unique = true, nullable = false)
        val username: String,

        @Column(nullable = false)
        val name: String,

        @Column(nullable = false)
        val lastname: String,

        val phone: String? = null,

        @Column(unique = true, nullable = false)
        val email: String,

        @Column(nullable = false)
        val password: String,

        @Column(nullable = false)
        val role: String,

        @Column(nullable = false)
        val active: Boolean
    )