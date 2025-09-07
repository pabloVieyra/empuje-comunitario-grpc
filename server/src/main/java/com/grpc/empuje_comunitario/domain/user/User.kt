package com.grpc.empuje_comunitario.domain.user

import java.util.Objects

class User private constructor(
    val id: UserId,
    val username: String,
    var name: String,
    var lastname: String,
    var phone: String,
    val email: String,
    var role: Role,
    var active: Boolean,
    private val emailValidator: FieldValidator,
    private val requiredFieldValidator: FieldValidator
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
                UserId.newId(),
                username.trim(),
                name.trim(),
                lastname.trim(),
                phone,
                email,
                role,
                true,
                EmailValidator(),
                RequiredFieldValidator()
            )
        }
    }
}