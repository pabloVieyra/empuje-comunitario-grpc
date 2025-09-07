package com.grpc.empuje_comunitario.domain.user

    class RequiredFieldValidator : FieldValidator {
        override fun validate(value: String?): Boolean {
            return value != null && value.trim().isNotEmpty()
        }
    }