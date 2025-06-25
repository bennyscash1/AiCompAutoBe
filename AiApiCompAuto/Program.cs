var builder = WebApplication.CreateBuilder(args);

// בודק אם אנחנו בסביבת פיתוח או בענן
if (builder.Environment.IsDevelopment())
{
    // HTTPS מקומי
    builder.WebHost.UseUrls("https://localhost:7012");
}
else
{
    // Azure מחייב HTTP על פורט 8080
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

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

// MCP
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
