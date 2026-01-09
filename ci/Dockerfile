# https://github.com/JeremiahSanders/ci-env-dotnet/blob/main/Dockerfile
FROM gnosian/ci-env-dotnet:latest AS build-environment

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS net-sdk-6

FROM build-environment AS with-net-6
COPY --from=net-sdk-6 /usr/share/dotnet /usr/share/dotnet
