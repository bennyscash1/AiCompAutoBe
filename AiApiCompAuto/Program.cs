using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// הוספת שירותי API
builder.Services.AddControllers();

// קביעת פורט דינמית לפי סביבה
var port = Environment.GetEnvironmentVariable("PORT");
if (string.IsNullOrEmpty(port))
{
    // מצב פיתוח מקומי עם HTTPS
    builder.WebHost.UseUrls("https://localhost:7012");
}
else
{
    // פורט שניתן על ידי Azure
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

var app = builder.Build();

// ברירת מחדל לבדיקה שהשרת פעיל
app.MapGet("/", () => "✅ Qshure API is running!");

// מיפוי שאר ה־API
app.MapControllers();

app.Run();
