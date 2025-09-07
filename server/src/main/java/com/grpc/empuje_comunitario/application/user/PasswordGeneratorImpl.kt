package com.grpc.empuje_comunitario.application.user

import org.springframework.stereotype.Component
import java.security.SecureRandom

@Component
class PasswordGeneratorImpl(
    private val encrypter: Encrypter
) : PasswordGenerator {

    companion object {
        private const val CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#\$%^&*()"
        private val random = SecureRandom()
        private const val PASSWORD_LENGTH = 10
    }

    // TODO: Improve password generation security
    override fun generateRandomPassword(): String {
        val sb = StringBuilder(PASSWORD_LENGTH)
        repeat(PASSWORD_LENGTH) {
            val randomIndex = random.nextInt(CHARS.length)
            sb.append(CHARS[randomIndex])
        }
        return encrypter.encrypt(sb.toString())
    }
}