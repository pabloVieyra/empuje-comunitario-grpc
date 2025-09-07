package com.grpc.empuje_comunitario.application.user

interface Encrypter {
    fun encrypt(data: String): String
}