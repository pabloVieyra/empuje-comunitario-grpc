package com.grpc.empuje_comunitario.infrastructure.persistence

import jakarta.persistence.*
import java.io.Serializable
import java.time.LocalDateTime // <-- ¡Importación faltante!

@Entity
@Table(name = "external_adhesions")
@IdClass(ExternalAdhesionId::class)
class ExternalAdhesionEntity(

    @Id
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "external_event_id", nullable = false)

    var event: ExternalEventEntity? = null,

    @Id
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id", nullable = false)
    var user: UserEntity? = null,

    @Column(name = "adhesion_date", nullable = false)
    var adhesionDate: LocalDateTime = LocalDateTime.now() // Debe ser 'var' si se permite nulo

) : Serializable {

    constructor() : this(null, null, LocalDateTime.now())

    constructor(event: ExternalEventEntity, user: UserEntity) :
            this(event, user, LocalDateTime.now())
}

class ExternalAdhesionId : Serializable {
    var event: Int? = null // Corresponde al campo 'id' de ExternalEventEntity
    var user: String? = null // Corresponde al campo 'id' de UserEntity


    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (javaClass != other?.javaClass) return false

        other as ExternalAdhesionId

        if (event != other.event) return false
        if (user != other.user) return false

        return true
    }

    override fun hashCode(): Int {
        var result = event.hashCode()
        result = 31 * result + user.hashCode()
        return result
    }
}