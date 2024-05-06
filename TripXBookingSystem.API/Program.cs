using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TripxBookingSystem.Factories.Implementations;
using TripxBookingSystem.Factories.Interfaces;
using TripXBookingSystem.DataStore.BaseDataStore;
using TripXBookingSystem.DataStore.Interfaces;
using TripXBookingSystem.Exceptions;
using TripXBookingSystem.Models.Book;
using TripXBookingSystem.Services.Implementations;
using TripXBookingSystem.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IManagerFactory, ManagerFactory>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICheckStatusService, CheckStatusService>();
builder.Services.AddScoped<IDataStore<BookRes>, BaseDataStore<BookRes>>();
builder.Services.AddScoped<IDataStore<string>, BaseDataStore<string>>();
builder.Services.AddScoped<IHotelAndFlightService, HotelAndFlightService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHttpRequestService, HttpRequestService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:44380",
            ValidAudience = "TestAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere1234567891234567891234"))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TripXBookingSystem", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization header using the Bearer scheme. Enter your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.UseMiddleware<AppExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseWhen(context => context.Request.Path.StartsWithSegments("/auth/token"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        await next.Invoke();
    });
});

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization();
});

app.MapControllers();

app.Run();
