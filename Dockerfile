FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

LABEL version="4.0.0" description="new asp net ddd version"

WORKDIR /app

COPY src/ .

WORKDIR /app/backend/TatooMarket.Api

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build-env /app/out .

COPY --from=build-env /app/backend/TatooMarket.Api/appsettings.json .

ENTRYPOINT [ "dotnet", "TatooMarket.Api.dll" ]

EXPOSE 8080
