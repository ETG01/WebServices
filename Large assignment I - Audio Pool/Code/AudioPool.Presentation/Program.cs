using Microsoft.EntityFrameworkCore;
using AudioPool.Repository.Data;
using AudioPool.Service;
// Add using directive for your repository and service namespaces
using AudioPool.Repository.Interfaces;
using AudioPool.Service.Interfaces;
using AudioPool.Service.Services;
using AudioPool.Presentation.Profiles;
using AudioPool.Repository.Implementations;
using System.Reflection;
using System.IO;
using System;

using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Adding DbContext service
builder.Services.AddDbContext<AudioPoolDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AudioPoolDatabase"),
        b => b.MigrationsAssembly("AudioPool.Presentation")));

// Registering the Repository's
builder.Services.AddScoped<IGenresRepository, GenresRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ISongsRepository, SongsRepository>();

// Registering the Service's
builder.Services.AddScoped<IGenresService, GenresService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ISongsService, SongsService>();

// Register AutoMapper
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<TimeSpanSchemaFilter>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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