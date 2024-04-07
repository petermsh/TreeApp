using Microsoft.EntityFrameworkCore;
using TreeApp.DAL;
using TreeApp.Interfaces;
using TreeApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddPostgres(builder.Configuration);

builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<INodeService, NodeService>();

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:5173")
            .SetIsOriginAllowed((host) => true);;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();