using Microsoft.EntityFrameworkCore;
using Shop.Database;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration["DefaultConnection"]);
});

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Cart";
    options.Cookie.MaxAge = TimeSpan.FromDays(7);
});

StripeConfiguration.ApiKey = configuration.GetSection("Stripe")["SecretKey"];

var options = new PaymentIntentCreateOptions
{
    Amount = 1099,
    Currency = "usd",
    PaymentMethodTypes = new List<string> { "link", "card" },
};
var service = new PaymentIntentService();
service.Create(options);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();