package com.grpc.empuje_comunitario.repository

interface Encrypter {
    fun encrypt(data: String): String
}