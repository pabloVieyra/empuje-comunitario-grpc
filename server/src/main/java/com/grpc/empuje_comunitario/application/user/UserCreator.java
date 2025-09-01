package com.grpc.empuje_comunitario.application.user;

import com.grpc.empuje_comunitario.domain.user.*;
import io.grpc.Status;
import org.springframework.stereotype.Service;

@Service
public class UserCreator {

    private final UserRepository userRepository;
    private final PasswordEncryptor passwordEncryptor;
    private final EmailNotificationService emailNotificationService;

    public UserCreator(UserRepository userRepository,
                       PasswordEncryptor passwordEncryptor,
                       EmailNotificationService emailNotificationService) {
        this.userRepository = userRepository;
        this.passwordEncryptor = passwordEncryptor;
        this.emailNotificationService = emailNotificationService;
    }

    public void create(String usernameValue,
                       String name,
                       String lastname,
                       String phone,
                       String emailValue,
                       String roleString) {

        Role role;
        try {
            role = Role.valueOf(roleString);
        } catch (IllegalArgumentException e) {
            throw new IllegalArgumentException("Invalid role: " + roleString);
        }

        Username username = new Username(usernameValue);
        Email email = new Email(emailValue);

        validateUniqueConstraints(username, email);

        String plainPassword = PasswordGenerator.generateRandomPassword();
        String encryptedPassword = passwordEncryptor.encrypt(plainPassword);

        User user = User.create(username, name, lastname, phone, email, encryptedPassword, role);

        userRepository.save(user);

        notifyUserByEmail(user, plainPassword);
    }

    private void validateUniqueConstraints(Username username, Email email) {
        if (userRepository.existsByUsername(username)) {
            throw new IllegalArgumentException("Username already exists");
        }

        if (userRepository.existsByEmail(email)) {
            throw new IllegalArgumentException("Email already exists");
        }
    }

    private void notifyUserByEmail(User user, String plainPassword) {
        //TODO: pasar a un template de email en una clase
        String subject = "Bienvenido a Empuje Comunitario";
        String message = String.format(
                "Hola %s %s,\n\n" +
                        "Tu cuenta ha sido creada exitosamente.\n" +
                        "Tu nombre de usuario: %s\n" +
                        "Tu contraseña: %s\n\n" +
                        "Por favor, guarda esta información en un lugar seguro.\n\n" +
                        "Saludos,\n" +
                        "Equipo de Empuje Comunitario",
                user.name(), user.lastname(), user.username().value(), plainPassword
        );

        emailNotificationService.sendEmail(user.email().value(), subject, message);
    }
}