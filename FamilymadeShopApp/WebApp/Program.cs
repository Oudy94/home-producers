using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Interfaces.Discount;
using BusinessLogicLayer.Managers;
using BusinessLogicLayer.Managers.Discount;
using BusinessLogicLayer.Managers.Discount.Factory;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using ModelLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Login");
        options.AccessDeniedPath = new PathString("/Error");
    });


//Data Access
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

//Business Logic
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IOrderManager, OrderManager>();
builder.Services.AddScoped<ICartManager, CartManager>();
builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddScoped<IDiscountFactory, PercentageDiscountFactory>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
