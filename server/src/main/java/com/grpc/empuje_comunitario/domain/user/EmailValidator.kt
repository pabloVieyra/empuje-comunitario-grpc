package com.grpc.empuje_comunitario.domain.user

class EmailValidator : FieldValidator {
    override fun validate(value: String?): Boolean {
        return value != null && Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$").matches(value)
    }
}