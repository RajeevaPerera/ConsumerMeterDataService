FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ensek.ServiceHost.ConsumerMeterDataService/Ensek.ServiceHost.ConsumerMeterDataService.csproj", "Ensek.ServiceHost.ConsumerMeterDataService/"]
RUN dotnet restore "Ensek.ServiceHost.ConsumerMeterDataService/Ensek.ServiceHost.ConsumerMeterDataService.csproj"
COPY . .
WORKDIR "/src/Ensek.ServiceHost.ConsumerMeterDataService"
RUN dotnet build "Ensek.ServiceHost.ConsumerMeterDataService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Ensek.ServiceHost.ConsumerMeterDataService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ensek.ServiceHost.ConsumerMeterDataService.dll"]
