package com.grpc.empuje_comunitario.network

import com.grpc.empuje_comunitario.controller.DonationController
import com.grpc.empuje_comunitario.domain.MyResult
import com.grpc.empuje_comunitario.domain.donation.Donation
import com.grpc.empuje_comunitario.proto.*
import io.grpc.stub.StreamObserver
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.grpc.server.service.GrpcService
import org.springframework.transaction.annotation.Transactional
import java.time.LocalDateTime

@GrpcService
open class DonationGrpcService @Autowired constructor(
    private val donationInventoryController: DonationController
) : DonationInventoryServiceGrpc.DonationInventoryServiceImplBase() {

    @Transactional
    override fun createDonationInventory(
        request: CreateDonationInventoryRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = donationInventoryController.createDonationInventory(
            category = request.inventory.category.toString(),
            description = request.inventory.description,
            quantity = request.inventory.quantity,
            token = request.token
        )
        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun updateDonationInventory(
        request: UpdateDonationInventoryRequest,
        responseObserver: StreamObserver<UpdateDonationInventoryResponse>
    ) {
        val result = donationInventoryController.updateDonationInventory(
            id = request.id,
            category = request.category.toString(),
            description = request.description,
            quantity = request.quantity,
            token = request.token
        )
        when (result) {
            is MyResult.Success -> {
                val response = UpdateDonationInventoryResponse.newBuilder()
                    .setSuccess(true)
                    .setMessage("Inventario actualizado correctamente")
                    .setInventory(result.data.toProto())
                    .build()
                responseObserver.onNext(response)
            }
            is MyResult.Failure -> {
                val response = UpdateDonationInventoryResponse.newBuilder()
                    .setSuccess(false)
                    .setMessage(mapErrorMessage(result.error))
                    .build()
                responseObserver.onNext(response)
            }
        }
        responseObserver.onCompleted()
    }

    @Transactional
    override fun deleteDonationInventory(
        request: DeleteDonationInventoryRequest,
        responseObserver: StreamObserver<GenericResponse>
    ) {
        val result = donationInventoryController.disableDonationInventory(
            donationId = request.id,
            token = request.token
        )
        responseObserver.onNext(result.toGenericResponse())
        responseObserver.onCompleted()
    }

    @Transactional
    override fun listDonationInventory(
        request: ListDonationInventoryRequest,
        responseObserver: StreamObserver<ListDonationInventoryResponse>
    ) {
        val result = donationInventoryController.listDonationInventory(
            token = request.token
        )
        when (result) {
            is MyResult.Success -> {
                val response = ListDonationInventoryResponse.newBuilder()
                    .setSuccess(true)
                    .setMessage("Inventario listado correctamente")
                    .addAllInventories(result.data.map { it.toProto() })
                    .build()
                responseObserver.onNext(response)
            }
            is MyResult.Failure -> {
                val response = ListDonationInventoryResponse.newBuilder()
                    .setSuccess(false)
                    .setMessage(mapErrorMessage(result.error))
                    .build()
                responseObserver.onNext(response)
            }
        }
        responseObserver.onCompleted()
    }

    private fun MyResult<Unit>.toGenericResponse(): GenericResponse {
        return when (this) {
            is MyResult.Success -> GenericResponse.newBuilder()
                .setSuccess(true)
                .setMessage("Operación exitosa")
                .build()
            is MyResult.Failure -> GenericResponse.newBuilder()
                .setSuccess(false)
                .setMessage(mapErrorMessage(this.error))
                .build()
        }
    }

    private fun mapErrorMessage(error: Throwable): String =
        when {
            error is IllegalArgumentException -> "Error de validación: ${error.message}"
            else -> "Error: ${error.message ?: "Error desconocido"}"
        }

    private fun Donation.toProto(): DonationInventory {
        val builder = DonationInventory.newBuilder()
            .setId(this.idDonation)
            .setCategory(
                if (this.category.isNotEmpty()) DonationCategory.valueOf(this.category)
                else DonationCategory.ROPA // fallback
            )
            .setDescription(this.description)
            .setQuantity(this.quantity)
            .setDeleted(this.isDeleted)
            .setCreatedAt(this.creationDate.toString())
            .setCreatedBy(this.creationUser)
        if (this.modificationDate != null) builder.setUpdatedAt(this.modificationDate.toString())
        if (this.modificationUser != null) builder.setUpdatedBy(this.modificationUser)
        return builder.build()
    }
}