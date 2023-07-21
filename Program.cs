var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

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

app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");





app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "QuanLyArea",
       areaName: "QuanLy",
       pattern: "QuanLy/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
       name: "DoanVienArea",
       areaName: "DoanVien",
       pattern: "DoanVien/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}"); // This route is for Controllers which are situated in project controller folder
});

app.Run();

