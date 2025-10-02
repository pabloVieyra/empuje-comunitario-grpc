package com.grpc.empuje_comunitario.repository

import com.grpc.empuje_comunitario.infrastructure.persistence.DonationEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.EventEntity
import com.grpc.empuje_comunitario.infrastructure.persistence.UserEntity


interface EventNetworkDatabase {
    fun saveEvent(event: EventEntity): Boolean
    fun updateEvent(event: EventEntity): EventEntity
    fun findById(id: Int): EventEntity?
    fun findAll(): List<EventEntity>
    fun deleteEvent(event: EventEntity): Boolean
    fun addUserToEvent(event: EventEntity, user: UserEntity): Boolean
    fun removeUserFromEvent(event: EventEntity, user: UserEntity): Boolean
    fun registerDonation(event: EventEntity, donation: DonationEntity, quantity: Int, actor: UserEntity): Boolean
}