using DeckGameApi.Common.Interfaces;
using DeckGameApi.Infrastrucutre.Persistence;
using DeckGameApi.Infrastrucutre.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDeckGameRepository, DeckGameRepository>();
builder.Services.AddControllers();
builder.Services.AddDbContext<DeckGameDbContext>(
        options => options.UseInMemoryDatabase(databaseName: "GameDeckDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
