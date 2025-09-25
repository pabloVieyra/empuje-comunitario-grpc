package com.grpc.empuje_comunitario.infrastructure.security

import com.grpc.empuje_comunitario.repository.TokenGenerator
import com.grpc.empuje_comunitario.domain.user.User
import com.grpc.empuje_comunitario.domain.user.asString
import io.jsonwebtoken.Jwts
import io.jsonwebtoken.SignatureAlgorithm
import io.jsonwebtoken.security.Keys
import org.springframework.stereotype.Service
import java.util.*
import javax.crypto.SecretKey

@Service
open class JwtTokenGenerator : TokenGenerator {
    private val key: SecretKey

    init {
        this.key = Keys.hmacShaKeyFor(SECRET.toByteArray())
    }

    override fun generateToken(user: User): String {
        val nowMillis = System.currentTimeMillis()
        val now = Date(nowMillis)
        val expiry = Date(nowMillis + EXPIRATION_MILLIS)

        return Jwts.builder()
            .setSubject(user.id.toString())
            .claim("role", user.role.asString())
            .setIssuedAt(now)
            .setExpiration(expiry)
            .signWith(key, SignatureAlgorithm.HS256)
            .compact()
    }

fun validateAndGetSubjectAndRole(token: String?): Pair<Boolean, String?> {
    return try {
        val claims = Jwts.parserBuilder()
            .setSigningKey(key)
            .build()
            .parseClaimsJws(token)
            .body
        val role = claims["role"] as? String
        Pair(true, role)
    } catch (ex: Exception) {
        Pair(false, null)
    }
}

    companion object {
        private const val SECRET = "this-is-a-very-strong-secret-key-32-chars-minimum!"
        private const val EXPIRATION_MILLIS: Long = 3600000 // 1 hora
    }
}