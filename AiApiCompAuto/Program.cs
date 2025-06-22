var builder = WebApplication.CreateBuilder(args);

// קובע פורט קבוע לכל הרצה: גם ב־dotnet run וגם בקובץ EXE
builder.WebHost.UseUrls("https://localhost:7012");

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
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

// אפשר להשאיר אם לא עושה redirect ל-HTTPS
// או למחוק כדי להימנע מבלבול אם אין תעודת SSL
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
