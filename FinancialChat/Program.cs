using FinancialChat.Areas.Identity;
using FinancialChat.Data;
using FinancialChat.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using DataAccess.Data;
using DataAccess.DbAccess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using BotService;

var builder = WebApplication.CreateBuilder(args);
var rabbitMQUrl = builder.Configuration.GetSection("RabbitMQSettings").GetValue(typeof(string), "Url").ToString();
var rabbitMQQueue = builder.Configuration.GetSection("RabbitMQSettings").GetValue(typeof(string), "Queue").ToString();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<IMessageData, MessageData>();
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>(serviceProvider =>
	new RabbitMQService(serviceProvider, rabbitMQUrl, rabbitMQQueue));
builder.Services.AddSingleton(x => new Bot(rabbitMQUrl, rabbitMQQueue));
builder.Services.AddScoped<ChatroomMessagesManager>();

builder.Services.AddResponseCompression(options =>
{
	options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
		new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var rabbitMQService = (IRabbitMQService)app.Services.GetService(typeof(IRabbitMQService));
rabbitMQService.Connect();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<ChatHub>("/ChatHub");
app.MapFallbackToPage("/_Host");

app.Run();
