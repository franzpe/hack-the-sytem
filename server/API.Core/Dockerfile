#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["API.Core/API.Core.csproj", "API.Core/"]
RUN dotnet restore "API.Core/API.Core.csproj"
COPY . .
WORKDIR "/src/API.Core"
RUN dotnet build "API.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Core.dll"]