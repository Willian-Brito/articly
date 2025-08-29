FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Api/Api.csproj", "Presentation/Api/"]
COPY ["Infrastructure/IoC/IoC.csproj", "Infrastructure/IoC/"]
COPY ["Core/Domain/Domain.csproj", "Core/Domain/"]
COPY ["Core/Application/Application.csproj", "Core/Application/"]
COPY ["Infrastructure/Data/Data.csproj", "Infrastructure/Data/"]
COPY ["Infrastructure/Security/Security.csproj", "Infrastructure/Security/"]
RUN dotnet restore "Presentation/Api/Api.csproj"
COPY . .
WORKDIR "/src/Presentation/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
