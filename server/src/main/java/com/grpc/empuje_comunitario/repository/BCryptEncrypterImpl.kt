package com.grpc.empuje_comunitario.repository

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import org.springframework.stereotype.Component

@Component
class BCryptEncrypterImpl() : Encrypter {

    private val encoder = BCryptPasswordEncoder()

    override fun encrypt(data: String): String {
        return encoder.encode(data)
    }
}