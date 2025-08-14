FROM mcr.microsoft.com/dotnet/aspnet:8.0.18 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0.412 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY  ["Weblog.API/Weblog.API.csproj", "Weblog.API/"]
COPY  ["Weblog.Application/Weblog.Application.csproj", "Weblog.Application/"]
COPY  ["Weblog.Domain/Weblog.Domain.csproj", "Weblog.Domain/"]
COPY  ["Weblog.Infrastructure/Weblog.Infrastructure.csproj", "Weblog.Infrastructure/"]
COPY  ["Weblog.Persistence/Weblog.Persistence.csproj", "Weblog.Persistence/"]
RUN dotnet restore "Weblog.API/Weblog.API.csproj"
COPY . .    

WORKDIR "/src/Weblog.API"
RUN dotnet build "Weblog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Weblog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "Weblog.API.dll"]
