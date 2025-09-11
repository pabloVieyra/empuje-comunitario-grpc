package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity


interface NetworkDatabase {
    fun saveUser(user: UserEntity): Boolean
    fun findUserByEmail(email: String?): UserEntity?
    fun findAllUsers(): List<UserEntity>
}