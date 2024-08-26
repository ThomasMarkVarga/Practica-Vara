using Microsoft.EntityFrameworkCore;
using DbContextProj;
using DataRepositoryProject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowedSpecificOrigins = "_myAllowedSpecificOrigins";

builder.Services.AddCors( options =>
{
    options.AddPolicy(name: MyAllowedSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7045");
        });
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WebAPI_DB"));
});

builder.Services.AddTransient<IDataRepository, DataRepositoryDB>();

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

app.UseCors(MyAllowedSpecificOrigins);

app.Run();
