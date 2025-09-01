package com.grpc.empuje_comunitario.domain.user;

import java.util.regex.Pattern;

public final class Email {
    private static final Pattern P = Pattern.compile("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");

    private final String value;

    public Email(String value) {
        if (value == null || !P.matcher(value).matches()) {
            throw new IllegalArgumentException("Invalid email");
        }
        this.value = value.toLowerCase();
    }

    public String value() {
        return value;
    }

    @Override
    public String toString() {
        return value;
    }
}