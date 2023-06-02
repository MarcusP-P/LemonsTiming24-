using LemonsTiming24.Server.Infrastructure;
using LemonsTiming24.Server.Infrastructure.SocketIO;
using LemonsTiming24.Server.Model.Database.Context;
using LemonsTiming24.Server.Services.BackgroundProcessing;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CA1852 // Seal internal types
var builder = WebApplication.CreateBuilder(args);
#pragma warning restore CA1852 // Seal internal types

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "LemonsTiming24.Server", Version = "v1" }));

builder.Services.Configure<TimingConfiguration>(builder.Configuration.GetSection("Timing"));

builder.Services.AddHostedService<TimingDataFetcherHostedService>();
builder.Services.AddScoped<ITimingDataFetcher, TimingDataFetcher>();
//builder.Services.AddScoped<ITimingDataFetcher, TimingDataFetcherTest>();

builder.Services.AddScoped<HttpClientRequestTrace>();
builder.Services.AddScoped<DebuggingHttpClient>();

builder.Services.AddDbContext<TimingRawContext>(
    options =>
        _ = options.UseSqlServer(builder.Configuration.GetConnectionString("TimingRawContext")));

builder.Services.AddHttpClient("SocketIO")
    .AddHttpMessageHandler<HttpClientRequestTrace>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();

    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LemonsTiming24.Server v1"));
}
else
{
    _ = app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TimingRawContext>();
    _ = context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseWebSockets();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
