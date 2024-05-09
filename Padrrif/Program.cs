using Microsoft.OpenApi.Models;
using Padrrif;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => {

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
           .EnableDetailedErrors()
           .EnableSensitiveDataLogging()
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddOptions();
builder.Services.ConfigureOptions<JwtAccessOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IGovernorateUnitOfWork, GovernorateUnitOfWork>();
builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
builder.Services.AddScoped<INotifactionUnitOfWork, NotifactionUnitOfWork>();
builder.Services.AddScoped<IComitteeUnitOfWork, ComitteeUnitOfWork>();
builder.Services.AddScoped<IEducationLevelDtoUnitOfWork, EducationLevelDtoUnitOfWork>();
builder.Services.AddScoped<IVillageUnitOfWork, VillageUnitOfWork>();
builder.Services.AddScoped<IOwnerShipTypeUnitOfWork, OwnerShipTypeUnitOfWork>();
builder.Services.AddScoped<IDamageUnitOfWork, DamageUnitOfWork>();


builder.Services.AddSingleton<NotificationHubConecctedUsers>();

builder.Services.AddSignalR();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<NotificationHub>("/notification-Hub");

app.MapFallbackToFile("index.html");


app.Run();
