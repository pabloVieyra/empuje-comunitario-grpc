package com.grpc.empuje_comunitario.domain.user;

import java.util.Objects;

public class User {
    private final UserId id;
    private final Username username;
    private String name;
    private String lastname;
    private String phone;
    private final Email email;
    private final String password;
    private Role role;
    private boolean active;

    private User(UserId id,
                 Username username,
                 String name,
                 String lastname,
                 String phone,
                 Email email,
                 String password,
                 Role role,
                 boolean active) {

        this.id = Objects.requireNonNull(id);
        this.username = Objects.requireNonNull(username);

        if (name == null || name.isBlank())
            throw new IllegalArgumentException("Name is required");
        this.name = name.trim();

        if (lastname == null || lastname.isBlank())
            throw new IllegalArgumentException("Lastname is required");
        this.lastname = lastname.trim();

        this.phone = phone == null ? "" : phone.trim();
        this.email = Objects.requireNonNull(email);

        if (password == null || password.isBlank())
            throw new IllegalArgumentException("Password is required");
        this.password = password;

        this.role = Objects.requireNonNull(role);

        this.active = active;
    }

    public static User create(Username username,
                              String name,
                              String lastname,
                              String phone,
                              Email email,
                              String password,
                              Role role) {
        return new User(UserId.newId(), username, name, lastname, phone, email, password, role, true);
    }

    public UserId id() { return id; }
    public Username username() { return username; }
    public String name() { return name; }
    public String lastname() { return lastname; }
    public String phone() { return phone; }
    public Email email() { return email; }
    public String password() { return password; }
    public Role role() { return role; }
    public boolean isActive() { return active; }

    public void setLastname(String lastname) {
        this.lastname = lastname;
    }

    public void setPhone(String phone) {
        this.phone = phone;
    }

    public void setRole(Role role) {
        this.role = role;
    }

    public void setActive(boolean active) {
        this.active = active;
    }

    public void setName(String name) {
        this.name = name;
    }
}