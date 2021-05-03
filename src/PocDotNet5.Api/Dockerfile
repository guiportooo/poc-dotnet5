#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PocDotNet5.Api/PocDotNet5.Api.csproj", "src/PocDotNet5.Api/"]
RUN dotnet restore "src/PocDotNet5.Api/PocDotNet5.Api.csproj"
COPY . .
WORKDIR "/src/src/PocDotNet5.Api"
RUN dotnet build "PocDotNet5.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PocDotNet5.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PocDotNet5.Api.dll"]