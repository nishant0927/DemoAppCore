using ASPCoreWebApp.DB;
using ASPCoreWebApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContex>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddScoped(typeof(ICommonService<>), typeof(CommonService<>));
builder.Services.AddScoped<IItemService, ItemService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Item}/{action=DispalyList}/{id?}");

app.Run();
