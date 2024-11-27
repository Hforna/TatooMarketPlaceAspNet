FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL version="2.0.0" description="new asp net ddd version"
COPY out /app
WORKDIR /app
EXPOSE 8080
ENTRYPOINT [ "sh", "-c", "dotnet ef database update && dotnet TatooMarket.Api.dll" ]
