
using Business.Implementations;
using Business.Interfaces;
using Infraestructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPartidosServices, PartidosServices>();

builder.Services.AddDbContext<LigaFutContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")), ServiceLifetime.Scoped);
// permitir que otras aplicaciones consuman esta API
builder.Services.AddCors(options => options.AddPolicy("", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowWebApp", builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
