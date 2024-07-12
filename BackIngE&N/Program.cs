using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BackIngE_N.Config.Jwt;
using BackIngE_N.Logic;
using BackIngE_N.Context;
using BackIngE_N.Daemons;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra HttpClient
builder.Services.AddHttpClient<ChannelLogic>();

//Agrega la inyección de dependencias
builder.Services.AddScoped<JwtConfig>();
builder.Services.AddScoped<UserrLogic>();
builder.Services.AddScoped<SecurityLogic>();
builder.Services.AddScoped<PlayListLogic>();
builder.Services.AddScoped<ChannelLogic>();
builder.Services.AddScoped<ExportImportLogic>();

//Agrega el contexto de la base de datos
builder.Services.AddDbContext<IngenieriaeynContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Agrega la configuración de CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
               builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
    options.AddPolicy("AllowIonic",
               builder => builder
               .WithOrigins("http://localhost:8101", "http://localhost:8100")
               .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH")
               .AllowAnyHeader());
});

//Agrega la configuración de JWT
string key = builder.Configuration.GetValue<string>("Jwt:Key");
byte[] keyBytes = Encoding.UTF8.GetBytes(key ?? "");

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

// Registra el Hosted Service
builder.Services.AddHostedService<CheckChannelsDaemon>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowIonic");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

