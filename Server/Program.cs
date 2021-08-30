using LemonsTiming24.Server.Infrastructure;
using LemonsTiming24.Server.Services.BackgroundProcessing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new() { Title = "LemonsTiming24.Server", Version = "v1" }));

builder.Services.Configure<TimingConfiguration>(builder.Configuration.GetSection("Timing"));

builder.Services.AddHostedService<TimingDataFetcherHostedService>();
//builder.Services.AddScoped<ITimingDataFetcher, TimingDataFetcher>();
builder.Services.AddScoped<ITimingDataFetcher, TimingDataFetcherTest>();

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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseWebSockets();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
