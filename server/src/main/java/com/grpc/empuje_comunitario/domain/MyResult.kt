package com.grpc.empuje_comunitario.domain

sealed class MyResult <out T> {
    data class Success<out T>(val data: T) : MyResult<T>()
    data class Failure(val error: Throwable) : MyResult<Nothing>()
}