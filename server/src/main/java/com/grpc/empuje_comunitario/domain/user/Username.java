package com.grpc.empuje_comunitario.domain.user;

public final class Username {
    private final String value;

    public Username(String value) {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("Username is required");
        }
        this.value = value.trim();
    }

    public String value() {
        return value;
    }

    @Override
    public String toString() {
        return value;
    }
}