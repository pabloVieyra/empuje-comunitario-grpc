package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.PasswordResult

interface PasswordGenerator {
    fun generateRandomPassword(): MyResult<PasswordResult>
}