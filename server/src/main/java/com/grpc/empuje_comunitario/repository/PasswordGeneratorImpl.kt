package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.PasswordResult
import org.springframework.stereotype.Component
import java.security.SecureRandom

@Component
class PasswordGeneratorImpl(
    private val encrypter: Encrypter = BCryptEncrypterImpl()
) : PasswordGenerator {

    companion object {
        private const val CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#\$%^&*()"
        private val random = SecureRandom()
        private const val PASSWORD_LENGTH = 10
    }

    // TODO: Improve password generation security
    override fun generateRandomPassword(): MyResult<PasswordResult> {
        try {
            val sb = StringBuilder(PASSWORD_LENGTH)
            repeat(PASSWORD_LENGTH) {
                val randomIndex = random.nextInt(CHARS.length)
                sb.append(CHARS[randomIndex])
            }
            return MyResult.Success(PasswordResult(sb.toString(), encrypter.encrypt(sb.toString())))
            //return PasswordResult(sb.toString(), encrypter.encrypt(sb.toString()))
        } catch (e: Exception) {
            return MyResult.Failure(e)
        }
    }
}