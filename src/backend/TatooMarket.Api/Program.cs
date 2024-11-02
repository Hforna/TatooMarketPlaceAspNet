


using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Infrastructure.DataEntity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddIdentity<User, RoleEntity>()
    .AddEntityFrameworkStores<ProjectDbContext>()
    .AddDefaultTokenProviders();
    
var dbConnect = builder.Configuration.GetConnectionString("sqlserverconnection");

builder.Services.AddDbContext<ProjectDbContext>(opt => opt.UseSqlServer(dbConnect));

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
