package com.grpc.empuje_comunitario.domain

import com.grpc.empuje_comunitario.domain.user.User

interface UserRepository {
    fun createWithPassword(user: User, password: String): MyResult<Unit>
    fun findAll(): MyResult<List<User>>
    fun updateUser(user: User): MyResult<User>
}