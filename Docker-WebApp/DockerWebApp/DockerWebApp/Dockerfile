#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["DockerWebApp/DockerWebApp.csproj", "DockerWebApp/"]
#RUN dotnet restore "./DockerWebApp/DockerWebApp.csproj"
#COPY . .
#WORKDIR "/src/DockerWebApp"
#RUN dotnet build "./DockerWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./DockerWebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "DockerWebApp.dll"]


#########################################################################################################################
# Stage:0 - Download the base image to host the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

#stage:1 - Download the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DockerWebApp.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o build

#stage:2 - Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o publish

#stage:3 - Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "DockerWebApp.dll"]


