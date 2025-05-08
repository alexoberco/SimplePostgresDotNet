## Índice

1. [Descripción del proyecto](#descripción-del-proyecto)
2. [Requisitos previos](#requisitos-previos)
3. [Estructura de carpetas](#estructura-de-carpetas)
4. [Despliegue con Docker](#despliegue-con-docker)
5. [Ejecutar localmente](#ejecutar-localmente)
6. [Acceso y pruebas](#acceso-y-pruebas)
7. [Detener y limpiar](#detener-y-limpiar)
8. [Desarrollo continuo](#desarrollo-continuo)

---

## Descripción del proyecto

Este repositorio alberga una **Web API** en .NET 9 que expone un CRUD completo para un catálogo de productos agrícolas. La API se conecta a **PostgreSQL** para el almacenamiento de datos y utiliza **Adminer** para una interfaz de administración web de la base de datos. Todo el entorno (base de datos, panel de administración y API) se orquesta fácilmente con **Docker Compose**.

---

## Requisitos previos

* **Docker** (Engine) instalado y en ejecución.
* **Docker Compose** disponible (`docker compose` o `docker-compose`).
* Puertos libres en tu máquina:

  * `5432` para PostgreSQL
  * `8080` para Adminer
  * `5279` para la Web API

---

## Estructura de carpetas

```
SimplePostgresDotNet/
├── docker-compose.yml
├── simplepostgressdotnet.sln
├── ProductsService.API/
│   ├── Dockerfile
│   ├── ProductsService.API.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Controllers/
│   │   └── ProductsController.cs
│   ├── Models/
│   │   └── Product.cs
│   └── Data/
│       └── ApplicationDbContext.cs
└── README.md
```

---

## Despliegue con Docker

1. **Clona** el repositorio y ve al directorio:

   ```bash
   git clone https://github.com/alexoberco/SimplePostgresDotNet.git
   cd SimplePostgresDotNet
   ```
2. **Construye** y **levanta** los contenedores:

   ```bash
   docker compose up --build -d
   ```
3. **Verifica** que todos los servicios estén activos:

   ```bash
   docker ps
   ```

Al final de este paso tendrás:

* PostgreSQL escuchando en `localhost:5432`.
* Adminer en `http://localhost:8080`.
* Tu Web API en `http://localhost:5279`.

---

## Ejecutar localmente

Si deseas ejecutar la API sin Docker, sigue estos pasos:

1. **Instala** el .NET 9 SDK desde [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download).
2. **Prepara** una base de datos PostgreSQL local (o remota) y crea la base de datos `baseTaller` con usuario `admin` y contraseña `admin`.
3. **Configura** la cadena de conexión en `ProductsService.API/appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=baseTaller;Username=admin;Password=admin"
     }
   }
   ```
4. **Restaura**, **migra** y **ejecuta** la aplicación:

   ```bash
   cd ProductsService.API
   dotnet restore
   dotnet tool install --global dotnet-ef         # si no lo tienes instalado
   dotnet ef database update                       # aplica migraciones
   dotnet run                                      # inicia la API
   ```
5. La API escuchará en `http://localhost:5279` y tendrás disponible Swagger UI en `/swagger`.

---

## Acceso y pruebas

### Swagger UI

Explora y prueba todos los endpoints con documentación interactiva:

```
http://localhost:5279/swagger
```

### Adminer

Interfaz web para gestionar la base de datos:

```
http://localhost:8080
```

* System: PostgreSQL
* Server: db (si es Docker) o localhost
* Username: admin
* Password: admin
* Database: baseTaller

### Postman / Insomnia

| Operación    | Método | URL                                       | Body JSON (cuando aplique)                              |
| ------------ | ------ | ----------------------------------------- | ------------------------------------------------------- |
| Listar todos | GET    | `http://localhost:5279/api/products`      | —                                                       |
| Obtener uno  | GET    | `http://localhost:5279/api/products/{id}` | —                                                       |
| Crear nuevo  | POST   | `http://localhost:5279/api/products`      | `{ "name":"...","description":"...","price":X }`        |
| Actualizar   | PUT    | `http://localhost:5279/api/products/{id}` | `{ "id":1,"name":"...","description":"...","price":X }` |
| Eliminar     | DELETE | `http://localhost:5279/api/products/{id}` | —                                                       |

> **Headers comunes**:
>
> ```
> Content-Type: application/json
> ```

---

## Detener y limpiar

* Detener contenedores (mantiene datos):

  ```bash
  docker compose down
  ```
* Detener y eliminar volúmenes (resetea la base de datos):

  ```bash
  docker compose down --volumes
  ```

---

## Desarrollo continuo

* Reconstruir y relanzar tras cambios:

  ```bash
  docker compose up --build -d
  ```
* Seguir logs de la API en tiempo real:

  ```bash
  docker compose logs -f api
  ```
