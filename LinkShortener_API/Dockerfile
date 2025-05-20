# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

# Expõe a porta padrão
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "LinkShortener.dll"]
