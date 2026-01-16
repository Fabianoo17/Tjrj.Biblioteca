var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("BibliotecaApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.ApiClient>();
builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.AutoresApiService>();
builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.AssuntosApiService>();
builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.LivrosApiService>();
builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.FormasCompraApiService>();
builder.Services.AddScoped<Tjrj.Biblioteca.Web.Services.RelatoriosApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
