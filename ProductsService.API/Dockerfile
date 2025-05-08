# 1) Build: restore, build y publish
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia csproj e instala dependencias
COPY ["ProductsService.API/ProductsService.API.csproj", "ProductsService.API/"]
RUN dotnet restore "ProductsService.API/ProductsService.API.csproj"                     

# Copia el resto del código
COPY . .
WORKDIR "/src/ProductsService.API"

# Publica en Release DESHABILITANDO analizadores para evitar NETSDK1064
RUN dotnet publish -c Release \
    -o /app/publish 
# 2) Runtime: imagen ligera con sólo el runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Configura Kestrel para escuchar en el puerto 80
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .                                               

EXPOSE 80                                                                      
ENTRYPOINT ["dotnet", "ProductsService.API.dll"]                              
