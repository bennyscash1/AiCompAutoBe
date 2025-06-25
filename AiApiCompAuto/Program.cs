using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// הוסף שירותי API בסיסיים
builder.Services.AddControllers();

// חשוב: Azure מחפש ב-port 8080
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

// נתיב ברירת מחדל לבדיקה ששרת חי
app.MapGet("/", () => "✅ Qshure API is running!");

// מיפוי שאר ה-API
app.MapControllers();

app.Run();
