package com.grpc.empuje_comunitario.repository

interface Encrypter {
    fun encrypt(data: String): String
    fun matches(raw: String, encrypted: String): Boolean
}