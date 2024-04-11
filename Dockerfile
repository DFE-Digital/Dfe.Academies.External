# Stage 1
ARG ASPNET_IMAGE_TAG=8.0.0-bookworm-slim
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

ENV DEBIAN_FRONTEND=noninteractive

COPY ./Dfe.Academies.External.Web/ ./Dfe.Academies.External.Web/

RUN --mount=type=secret,id=github_token dotnet nuget add source --username USERNAME --password $(cat /run/secrets/github_token) --store-password-in-clear-text --name github "https://nuget.pkg.github.com/DFE-Digital/index.json"
RUN dotnet restore Dfe.Academies.External.Web
RUN dotnet build Dfe.Academies.External.Web --no-restore
RUN dotnet publish Dfe.Academies.External.Web -c Release -o /app --no-restore

COPY ./script/web-docker-entrypoint.sh /app/docker-entrypoint.sh

# Stage 2
ARG ASPNET_IMAGE_TAG
FROM "mcr.microsoft.com/dotnet/aspnet:${ASPNET_IMAGE_TAG}" AS final
LABEL org.opencontainers.image.source=https://github.com/DFE-Digital/Dfe.Academies.External

COPY --from=build /app /app
WORKDIR /app
RUN chmod +x ./docker-entrypoint.sh
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80/tcp
