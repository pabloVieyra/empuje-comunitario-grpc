package com.grpc.empuje_comunitario.domain.user

sealed class Role {
    data object PRESIDENT : Role()
    data object VOCAL : Role()
    data object COORDINATOR : Role()
    data object VOLUNTEER : Role()
}

fun Role.asString(): String = when(this) {
    Role.PRESIDENT -> "PRESIDENT"
    Role.VOCAL -> "VOCAL"
    Role.COORDINATOR -> "COORDINATOR"
    Role.VOLUNTEER -> "VOLUNTEER"
}


fun String.toRole(): Role = when(this) {
    "PRESIDENT" -> Role.PRESIDENT
    "VOCAL" -> Role.VOCAL
    "COORDINATOR" -> Role.COORDINATOR
    "VOLUNTEER" -> Role.VOLUNTEER
    else -> throw IllegalArgumentException("Invalid role: $this")
}