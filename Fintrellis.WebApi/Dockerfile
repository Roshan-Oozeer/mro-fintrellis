FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7212
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Fintrellis.WebApi/Fintrellis.WebApi.csproj", "Fintrellis.WebApi/"]
COPY ["Fintrellis.Common/Fintrellis.Common.csproj", "Fintrellis.Common/"]
COPY ["Fintrellis.Models/Fintrellis.Models.csproj", "Fintrellis.Models/"]
COPY ["Fintrellis.Repositories/Fintrellis.Repositories.csproj", "Fintrellis.Repositories/"]
RUN dotnet restore "./Fintrellis.WebApi/./Fintrellis.WebApi.csproj"
COPY . .
WORKDIR "/src/Fintrellis.WebApi"
RUN dotnet build "Fintrellis.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fintrellis.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fintrellis.WebApi.dll"]
