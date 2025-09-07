package com.grpc.empuje_comunitario.application.user

import com.grpc.empuje_comunitario.domain.Result
import com.grpc.empuje_comunitario.domain.user.EmailNotificationService
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity
import org.springframework.stereotype.Repository

@Repository
open class UserRepositoryImpl(
    private val passwordGenerator: PasswordGenerator,
    private val networkDatabase: NetworkDatabase,
    private val emailNotificationService: EmailNotificationService
) : UserRepository {
    override fun create(user: User): Result {
        try {
            val password = passwordGenerator.generateRandomPassword()
            val userEntity = UserEntity(
                user.id.value(),
                user.username,
                user.name,
                user.lastname,
                user.phone,
                user.email,
                password,
                user.role.name,
                user.active
            )

            networkDatabase.saveUser(userEntity)
            notifyUserByEmail(user, password)
            return Result.SUCCESS
        } catch (e: Exception) {
            throw e
        }
    }

    private fun notifyUserByEmail(user: User, plainPassword: String) {
        val subject = "Bienvenido a Empuje Comunitario"
        val message = """
                    Hola ${user.name} ${user.lastname},
                
                    Tu cuenta ha sido creada exitosamente.
                    Tu nombre de usuario: ${user.username}
                    Tu contraseña: $plainPassword
                
                    Por favor, guarda esta información en un lugar seguro.
                
                    Saludos,
                    Equipo de Empuje Comunitario
                """.trimIndent()

        emailNotificationService.sendEmail(user.email, subject, message)
    }
}