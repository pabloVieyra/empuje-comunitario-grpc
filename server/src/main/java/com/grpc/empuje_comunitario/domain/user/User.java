package com.grpc.empuje_comunitario.domain.user;

import java.util.Objects;

public class User {
    private final UserId id;
    private final String username;
    private String name;
    private String lastname;
    private String phone;
    private final String email;
    private Role role;
    private boolean active;
    private final FieldValidator emailValidator;

    private User(UserId id,
                 String username,
                 String name,
                 String lastname,
                 String phone,
                 String email,
                 Role role,
                 boolean active,
                 EmailValidator emailValidator
    ) {
        this.emailValidator = emailValidator;

        this.id = Objects.requireNonNull(id);
        //this.username = Objects.requireNonNull(username);

        if(username == null|| username.isBlank())
            throw new IllegalArgumentException("Username is required");
        this.username = username.trim();

        if (name == null || name.isBlank())
            throw new IllegalArgumentException("Name is required");
        this.name = name.trim();

        if (lastname == null || lastname.isBlank())
            throw new IllegalArgumentException("Lastname is required");
        this.lastname = lastname.trim();

        this.phone = phone == null ? "" : phone.trim();
        if (!this.emailValidator.validate(email))
            throw new IllegalArgumentException("Invalid email");
        this.email = Objects.requireNonNull(email);

//        if (password == null || password.isBlank())
//            throw new IllegalArgumentException("Password is required");
//        this.password = password;

        this.role = Objects.requireNonNull(role);

        this.active = active;

    }

    public static User create(String username,
                              String name,
                              String lastname,
                              String phone,
                              String email,
                              Role role) {
        return new User(UserId.newId(), username, name, lastname, phone, email, role, true, new EmailValidator());
    }

    public UserId id() { return id; }
    public String username() { return username; }
    public String name() { return name; }
    public String lastname() { return lastname; }
    public String phone() { return phone; }
    public String email() { return email; }
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