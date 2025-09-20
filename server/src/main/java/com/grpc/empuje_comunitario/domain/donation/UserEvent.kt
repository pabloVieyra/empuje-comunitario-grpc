package com.grpc.empuje_comunitario.domain.donation

import com.grpc.empuje_comunitario.domain.user.User

class UserEvent(
    val user: User,
    val event: Event
)