using Humanizer;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// First we need to configure the session state,we will need to add a middleware for mataining the session
builder.Services.AddSession(options =>
{
    // We can add a session or other properties for the configuration. For example,.Options.Session timeout,but we will use idle timeout that should give us in seconds
    options.IdleTimeout = TimeSpan.FromSeconds(20);
    // Options.cookies.http only,this internally stores the data in cookies only. Then there is options.Cookies. Dot is essential to store them in the cookie. 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Once this middleware is added, we can use this middleware into our application after building it, with the UseSession Method
app.UseSession();

app.MapControllerRoute(
name: "default",
//pattern: "{controller=Home}/{action=Index}/{id?}");
// Here we are changing the default route from Home to our Customer controller 
    pattern: "{controller=Customer}/{action=Index}/{id?}");

app.Run();
