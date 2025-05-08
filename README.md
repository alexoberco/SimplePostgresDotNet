# API RESTful en .NET 8 con PostgreSQL y Docker

Este proyecto implementa una API RESTful para la gestión de productos agrícolas, utilizando .NET 8, PostgreSQL y Docker. La aplicación permite realizar operaciones CRUD sobre un catálogo de productos.

## 🚀 Requisitos

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [Insomnia](https://insomnia.rest/) o [Postman](https://www.postman.com/) para probar la API

## 🧪 Estructura del Proyecto

```plaintext
.
├── ProductsService.API/
│   ├── Controllers/
│   │   └── ProductsController.cs
│   ├── Models/
│   │   └── Product.cs
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── Program.cs
│   └── appsettings.Development.json
├── docker-compose.yml
└── README.md
```
## 🛠️ Configuración del Proyecto

1. Clonar el Repositorio
```bash
git clone https://github.com/alexoberco/SimplePostgresDotNet.git
cd SimplePostgresDotNet
```
2. Configurar la Cadena de Conexión
En el archivo `ProductsService.API/appsettings.Development.json`, configura la cadena de conexión a la base de datos PostgreSQL:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db;Port=5432;Username=postgres;Password=tu_contraseña;Database=productos_db"
  }
}
```
3. Crear y Aplicar Migraciones
Desde el directorio `ProductsService.API`, ejecuta los siguientes comandos para crear y aplicar las migraciones:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
## 🐳 Ejecutar con Docker Compose
1. Iniciar los Contenedores

``` bash
docker-compose up -d
```
2. Verificar los Contenedores en Ejecución
Para asegurarte de que los contenedores están corriendo, ejecuta:

``` bash
docker ps
```
Deberías ver algo similar a:
``` bashCONTAINER ID   IMAGE                    COMMAND                  CREATED             STATUS             PORTS                    NAMES
abc123def456   tu_usuario/tu_imagen     "dotnet ProductsServi…"   2 minutes ago       Up 2 minutes       0.0.0.0:8080->80/tcp     simplepostgresdotnet-adminer-1
def456abc123   postgres:latest          "docker-entrypoint.s…"   2 minutes ago       Up 2 minutes       0.0.0.0:5432->5432/tcp   simplepostgresdotnet-db-1
```

## Ejecutar el proyecto

para ejecutar el proyecto se debe ejecutar este coando:

``` bash
dotnet watch run --project ProductsService.API 
```

