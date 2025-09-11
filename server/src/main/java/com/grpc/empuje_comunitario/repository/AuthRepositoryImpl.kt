package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.AuthRepository
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.user.toUser
import org.springframework.stereotype.Repository

@Repository
open class AuthRepositoryImpl(
    private val networkDatabase: NetworkDatabase,
    private val encrypter: Encrypter
) : AuthRepository {
    override fun findUserByEmail(email: String): MyResult<User> {
        return try {
            val entity = networkDatabase.findUserByEmail(email)
            if (entity != null) MyResult.Success(entity.toUser())
            else MyResult.Failure(Exception("Usuario o email inexistente"))
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }

    override fun checkPassword(email: String, password: String): MyResult<Unit> {
        return try {
            val userEntity = networkDatabase.findUserByEmail(email)
            if (userEntity == null) {
                MyResult.Failure(Exception("Usuario o email inexistente"))
            } else if (encrypter.matches(password, userEntity.password)) {
                MyResult.Success(Unit)
            } else {
                MyResult.Failure(Exception("Credenciales incorrectas"))
            }
        } catch (e: Exception) {
            MyResult.Failure(e)
        }
    }
}