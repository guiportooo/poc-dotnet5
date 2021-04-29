FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-amd64 AS base
WORKDIR /app
EXPOSE 5000/tcp
EXPOSE 5001/tcp

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PocDotNet5.Api/PocDotNet5.Api.csproj", "PocDotNet5.Api/"]

RUN dotnet restore "PocDotNet5.Api/PocDotNet5.Api.csproj"

WORKDIR /src
COPY . .
RUN dotnet build "src/PocDotNet5.Api/PocDotNet5.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src
RUN dotnet publish "src/PocDotNet5.Api/PocDotNet5.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PocDotNet5.Api.dll"]