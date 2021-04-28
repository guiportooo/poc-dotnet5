FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PocDotNet5.Api.csproj", "PocDotNet5.Api/"]
RUN dotnet restore "PocDotNet5.Api/PocDotNet5.Api.csproj"

WORKDIR /src/PocDotNet5.Api
COPY . .
RUN dotnet build "PocDotNet5.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/PocDotNet5.Api
RUN dotnet publish "PocDotNet5.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PocDotNet5.Api.dll"]
