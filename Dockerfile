# ASP.NET Core runtime image from Microsoft's registry. The pipeline publishes first (dotnet publish -> publish/).
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app
COPY publish/ .

ARG APP_VERSION=dev
ENV APP_VERSION=${APP_VERSION} \
    ASPNETCORE_URLS=http://+:8080
LABEL org.opencontainers.image.title="dotnet-vendor" \
      org.opencontainers.image.version="${APP_VERSION}"

USER app
EXPOSE 8080
ENTRYPOINT ["dotnet", "DotnetVendor.dll"]
