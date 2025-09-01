package com.grpc.empuje_comunitario.domain.user;

public enum Role {
    PRESIDENT,   // gestiona usuarios + todo lo demás
    VOCAL,       // inventario de donaciones
    COORDINATOR, // coordina eventos solidarios
    VOLUNTEER    // consulta eventos para participar
}