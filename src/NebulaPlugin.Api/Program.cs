using NebulaPlugin.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// builder.Services.AddHttpContextAccessor();

builder.Services.AddServices();
builder.Services.ConfigureSqlConnection(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
// app.UseHsts();

app.UseHttpsRedirection();

app.MapControllers();


app.MapGet("/health", () =>
{
    return "im ok";
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
