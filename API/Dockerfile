FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN sudo apt-get update
RUN sudo apt-get install libgdiplus
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API.csproj", "."]
RUN dotnet restore "./././API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]