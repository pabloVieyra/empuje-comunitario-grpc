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
            val success = networkDatabase.saveUser(user.toUserEntity(password))
            return if(success) MyResult.Success(Unit) else MyResult.Failure(Exception("Failed to save user"))
        } catch (e: Exception) {
            return MyResult.Failure(e)
        }
    }

    override fun findAll(): MyResult<List<User>> {
        TODO("Not yet implemented")
    }
}