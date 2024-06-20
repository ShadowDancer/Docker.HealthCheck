# Docker.HealthCheck
.NET tool that can serve as a curl health check replacement in Docker environments using chiseled images


## How to use

Example dockerfile:

```
FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build

RUN dotnet tool install --global Docker.HealthCheck

// Build your app here


FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled AS runtime

COPY --from=build /root/.dotnet/tools/ /tools/
HEALTHCHECK CMD ["/tools/Docker.HealthCheck", "http://localhost:8080/healthz/ready"]
```