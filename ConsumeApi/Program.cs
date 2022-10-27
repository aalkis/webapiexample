var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();
