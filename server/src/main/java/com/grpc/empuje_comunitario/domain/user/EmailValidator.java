package com.grpc.empuje_comunitario.domain.user;

public class EmailValidator implements FieldValidator{
    @Override
    public boolean validate(String value) {
        return value != null && value.matches("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
    }
}
