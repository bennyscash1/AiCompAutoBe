var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.WebHost.UseUrls("https://localhost:7012");
else
    builder.WebHost.UseUrls($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT") ?? "8080"}");

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ?? הסר זמנית את MCP
// builder.Services
//     .AddMcpServer()
//     .WithStdioServerTransport()
//     .WithToolsFromAssembly();

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();

// דף לבדיקה
app.MapGet("/", () => "API is running");

app.Run();
