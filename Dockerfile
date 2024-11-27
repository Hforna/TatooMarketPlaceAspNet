FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL version="2.0.0" description="new asp net ddd version"
COPY out /app
WORKDIR /app
EXPOSE 3000/tcp
ENTRYPOINT [ "dotnet", "TatooMarket.Api.dll" ]
