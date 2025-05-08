using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductsService.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuración de servicios

// Registra el DbContext para PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  

// Registra los controladores de Web API
builder.Services.AddControllers();                                                        

// Habilita la exploración de endpoints para OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();

// Configura SwaggerGen con metadatos básicos
builder.Services.AddSwaggerGen(c => {                                                     
    c.SwaggerDoc("v1", new OpenApiInfo {
        Version     = "v1",
        Title       = "Products API",
        Description = "CRUD para el catálogo de productos agrícolas"
    });
});

var app = builder.Build();

// 2. Configuración del pipeline HTTP

if (app.Environment.IsDevelopment())
{
    // Detalle de errores en desarrollo
    app.UseDeveloperExceptionPage();

    // Activa Swagger y su UI
    app.UseSwagger();                                                                     
    app.UseSwaggerUI(c =>                                                                
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapea automáticamente todas las rutas definidas con atributos en los controladores
app.MapControllers();                                                                     
app.Run();