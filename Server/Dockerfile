#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["nuget.config", "."]
COPY ["Server/LemonsTiming24.Server.csproj", "Server/"]
COPY ["Client/LemonsTiming24.Client.csproj", "Client/"]
COPY ["Shared/LemonsTiming24.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/LemonsTiming24.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "LemonsTiming24.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LemonsTiming24.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LemonsTiming24.Server.dll"]