package com.grpc.empuje_comunitario.application.user

import com.grpc.empuje_comunitario.infrastructure.security.BCryptPasswordEncryptor
import org.springframework.stereotype.Component

@Component
class EncrypterImpl(
    private val encryptor: BCryptPasswordEncryptor
) : Encrypter {
    override fun encrypt(data: String): String {
        return encryptor.encrypt(data)
    }
}