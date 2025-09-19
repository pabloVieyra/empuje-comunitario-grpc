FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV GRPC_TOOLS_PROTOC_EXEC=linux_x64
COPY . .
RUN dotnet restore EmpujeComunitario.Client.Api/EmpujeComunitario.Client.Api.csproj
RUN dotnet publish EmpujeComunitario.Client.Api/EmpujeComunitario.Client.Api.csproj -c Release -o /app/out --no-restore

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "EmpujeComunitario.Client.Api.dll"]