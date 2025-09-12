package com.grpc.empuje_comunitario.domain.user

import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import java.util.Objects

class User(
    val id: UserId,
    val username: String,
    var name: String,
    var lastname: String,
    var phone: String,
    val email: String,
    var role: Role,
    var active: Boolean,
    private val emailValidator: FieldValidator = EmailValidator(),
    private val requiredFieldValidator: FieldValidator = RequiredFieldValidator()
) {

    init {
        Objects.requireNonNull(id)

        if (!requiredFieldValidator.validate(username))
            throw IllegalArgumentException("Username is required")

        if (!requiredFieldValidator.validate(name))
            throw IllegalArgumentException("Name is required")

        if (!requiredFieldValidator.validate(lastname))
            throw IllegalArgumentException("Lastname is required")

        phone = phone?.trim() ?: ""

        if (!emailValidator.validate(email))
            throw IllegalArgumentException("Invalid email")

        Objects.requireNonNull(email)

        Objects.requireNonNull(role)
    }


    // TODO: deprecated , llevar el ID
    companion object {
        fun create(
            username: String,
            name: String,
            lastname: String,
            phone: String,
            email: String,
            role: Role
        ): User {
            return User(
                //TODO: delegar al repositorio
                UserId.newId(),
                username.trim(),
                name.trim(),
                lastname.trim(),
                phone,
                email,
                role,
                true
            )
        }
    }
}


fun UserEntity.toUser(): User {
    return User(
        id = UserId.from(this.id),
        username = this.username,
        name = this.name,
        lastname = this.lastname,
        phone = this.phone ?: "",
        email = this.email,
        role = this.role.toRole(),
        active = this.active,
    )
}