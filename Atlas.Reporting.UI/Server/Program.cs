using Atlas.Reporting.DAL.Domain.InternalSendingModel;
using Atlas.Reporting.DAL.Domain.OrderModel;
using Atlas.Reporting.DAL.Domain.SnappFoodModel;
using Atlas.Reporting.DAL.Services;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<OrderContext>
    (c =>
    {
        c.UseSqlServer(builder.Configuration.GetConnectionString("OrderCnn"));
    });
builder.Services.AddDbContext<SnappFoodContext>
    (c =>
    {
        c.UseSqlServer(builder.Configuration.GetConnectionString("SnappFoodCnn"));
    });
builder.Services.AddDbContext<InternalSendingContext>
    (c =>
    {
        c.UseSqlServer(builder.Configuration.GetConnectionString("InternalCnn"));
    });
builder.Services.AddScoped<IReportingService, ReportingService>();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
