package com.grpc.empuje_comunitario.domain.user;

public final class UserId {
    private final String value;

    private UserId(String value) {
        this.value = value;
    }

    public static UserId newId() {
        return new UserId(java.util.UUID.randomUUID().toString());
    }

    public String value() {
        return value;
    }
}