#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Source/DsuDev.BusinessDays.Services.API/DsuDev.BusinessDays.Services.API.csproj", "Source/DsuDev.BusinessDays.Services.API/"]
RUN dotnet restore "Source/DsuDev.BusinessDays.Services.API/DsuDev.BusinessDays.Services.API.csproj"
COPY . .
WORKDIR "/src/Source/DsuDev.BusinessDays.Services.API"
RUN dotnet build "DsuDev.BusinessDays.Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DsuDev.BusinessDays.Services.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DsuDev.BusinessDays.Services.API.dll"]