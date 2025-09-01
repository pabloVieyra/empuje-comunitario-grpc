import grpc
import proto.user_service_pb2 as user_service_pb2
import proto.user_service_pb2_grpc as user_service_pb2_grpc

def run():
    channel = grpc.insecure_channel("localhost:9090")
    stub = user_service_pb2_grpc.UserServiceStub(channel)

    request = user_service_pb2.CreateUserRequest(
        username="Lean123",
        name="Leandro",
        lastname="Mussi",
        phone="123456799",
        email="Lean@mail.com",
        role="PRESIDENT"
    )

    response = stub.CreateUser(request)
    print("Respuesta del servidor:")
    print(f"success={response.success}, message={response.message}")

if __name__ == "__main__":
    run()
