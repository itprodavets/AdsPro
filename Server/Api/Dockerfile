FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Server/Api/Api.csproj", "Server/Api/"]
COPY ["Server/Application/Application.csproj", "Server/Application/"]
COPY ["Server/Infrastructure/Infrastructure.csproj", "Server/Infrastructure/"]
COPY ["Server/Domain/Domain.csproj", "Server/Domain/"]

RUN dotnet restore "Server/Api/Api.csproj"
COPY . .
WORKDIR "/src/Server/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
