using Atlas.Reporting.DAL.Domain.InternalSendingModel;
using Atlas.Reporting.DAL.Domain.OrderModel;
using Atlas.Reporting.DAL.Domain.SnappFoodModel;
using Atlas.Reporting.DAL.Services;
using Atlas.Reporting.UI.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
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
builder.Services.AddScoped<ReportingService>();
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
