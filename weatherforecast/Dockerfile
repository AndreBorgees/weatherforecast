#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["weatherforecast/weatherforecast.csproj", "weatherforecast/"]
RUN dotnet restore "weatherforecast/weatherforecast.csproj"
COPY . .
WORKDIR "/src/weatherforecast"
RUN dotnet build "weatherforecast.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "weatherforecast.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "weatherforecast.dll"]