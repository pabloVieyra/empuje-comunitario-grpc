package com.grpc.empuje_comunitario.controller

data class TokenValidationResult(
    val valid: Boolean,
    val id: String?,
    val role: String?
)