using Microsoft.EntityFrameworkCore;
using GestionUsuarios.Data;
using GestionUsuarios.Models;
using GestionUsuarios.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Configura la base de datos SQLite con el DbContext
builder.Services.AddDbContext<UsuarioDbContext>(options =>
    options.UseSqlite("Data Source=usuarios.db"));

// Añade el generador de endpoints y la documentación Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirección HTTPS
app.UseHttpsRedirection();

// Aplicar migraciones y ejecutar el seeder al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UsuarioDbContext>();
    
    // Aplica las migraciones a la base de datos
    await dbContext.Database.MigrateAsync();
    
    // Poblar la base de datos con el Seeder
    await UsuarioSeeder.SeedAsync(dbContext);
}

// Ejecuta la aplicación
app.Run();
