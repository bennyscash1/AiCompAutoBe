var builder = WebApplication.CreateBuilder(args);

// ���� ���� ���� ��� ����: �� ��dotnet run ��� ����� EXE
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

// ���� ������ �� �� ���� redirect �-HTTPS
// �� ����� ��� ������ ������ �� ��� ����� SSL
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
