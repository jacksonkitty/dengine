#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 4410
#EXPOSE 4411
ENV ASPNETCORE_HTTP_PORTS=80;4410
#ENV ASPNETCORE_HTTPS_PORTS=443;4411

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/QuickConfig/QuickConfig.csproj", "QuickConfig/"]
RUN dotnet restore "./QuickConfig/./QuickConfig.csproj"
COPY /src .
WORKDIR "/src/QuickConfig"
RUN dotnet build "./QuickConfig.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuickConfig.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickConfig.dll"]