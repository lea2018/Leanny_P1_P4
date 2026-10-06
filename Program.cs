using Leanny_P1_P4.Data;
using Leanny_P1_P4.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddScoped<Database>();
builder.Services.AddScoped<DatabaseInitializer>();
builder.Services.AddScoped<AutoresService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider
        .GetRequiredService<DatabaseInitializer>();

    await initializer.InitializeAsync();
}

    app.MapOpenApi();
    app.MapScalarApiReference();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
