using ELibrary.API;
using ELibrary.API.Middleware;
using ELibrary.Business;
using ELibrary.Business.Helpers;
using ELibrary.DataAccess;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Sentry
builder.WebHost.UseSentry(o =>
{
    o.Dsn = "https://3a162b8228d90c980258b7679ee2f9f8@o4511341052690432.ingest.us.sentry.io/4511341060030464";
    o.Debug = false;
    o.TracesSampleRate = 1.0;
});

// Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication(builder.Environment, builder.Configuration)
                .AddDataAccess(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
