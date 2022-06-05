using BitwiseService;
using Core.Caching;
using Core.Configuration;
using DB.Entities.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//.Net Core�da, Dependency Injection ile ilgili DB Context�in aya�a kald�r�lmas� i�in, Startup.cs�de a�a��daki kodun tan�mlanmas� gerekmektedir.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<BitwiseConfig>(builder.Configuration.GetSection("BitwiseConfig"));
builder.Services.AddDbContext<BitwiseContext>(options => options.UseSqlServer(connString));
builder.Services.AddControllers();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRedisCacheService, RedisCacheService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
