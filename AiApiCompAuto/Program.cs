var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("https://localhost:7012");
builder.WebHost.UseUrls("https://0.0.0.0:7012");

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
