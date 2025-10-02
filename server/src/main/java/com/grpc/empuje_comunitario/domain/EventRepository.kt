package com.grpc.empuje_comunitario.domain

import com.grpc.empuje_comunitario.domain.donation.Event

interface EventRepository {
    fun create(event: Event): MyResult<Unit>
    fun update(event: Event): MyResult<Event>
    fun delete(eventId: Int, modificationUserId: String): MyResult<Unit>
    fun findById(eventId: Int): MyResult<Event>
    fun findAll(): MyResult<List<Event>>
    fun addUser(eventId: Int, userId: String, actorId: String): MyResult<Unit>
    fun removeUser(eventId: Int, userId: String, actorId: String): MyResult<Unit>
    fun registerDonation(eventId: Int, donationId: String, quantity: Int, actorId: String): MyResult<Unit>
}