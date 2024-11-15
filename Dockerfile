# Set the major version of dotnet
ARG DOTNET_VERSION=8.0

# Stage 1 - Build the app using the dotnet SDK
FROM "mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-azurelinux3.0" AS build
WORKDIR /build

COPY ./Dfe.Academies.External.Web/ ./Dfe.Academies.External.Web/
COPY ./script/web-docker-entrypoint.sh /app/docker-entrypoint.sh

# Mount GitHub Token as a Docker secret so that NuGet Feed can be accessed
RUN --mount=type=secret,id=github_token dotnet nuget add source --username USERNAME --password $(cat /run/secrets/github_token) --store-password-in-clear-text --name github "https://nuget.pkg.github.com/DFE-Digital/index.json"

# Restore, build and publish the dotnet solution
RUN ["dotnet", "restore", "Dfe.Academies.External.Web"]
RUN ["dotnet", "build", "Dfe.Academies.External.Web", "--no-restore"]
RUN ["dotnet", "publish", "Dfe.Academies.External.Web", "--no-restore", "-c", "Release", "-o", "/app"]

# Stage 2 - Build a runtime environment
FROM "mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-azurelinux3.0" AS final
WORKDIR /app
LABEL org.opencontainers.image.source="https://github.com/DFE-Digital/Dfe.Academies.External"

COPY --from=build /app /app
RUN ["chmod", "+x", "./docker-entrypoint.sh"]

USER $APP_UID
ENV ASPNETCORE_HTTP_PORTS=80
EXPOSE 80/tcp
