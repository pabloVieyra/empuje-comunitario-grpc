package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.UserRepository
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.infrastructure.persistence.toUserEntity
import org.springframework.stereotype.Repository

@Repository
open class UserRepositoryImpl(
    private val networkDatabase: NetworkDatabase
) : UserRepository {

    override fun createWithPassword(user: User, password: String): MyResult<Unit> {
        try {
            networkDatabase.saveUser(user.toUserEntity(password))
            return MyResult.Success(Unit)
        } catch (e: Exception) {
            return MyResult.Failure(e)
        }
    }
}