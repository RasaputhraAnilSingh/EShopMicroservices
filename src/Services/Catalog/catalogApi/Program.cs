using catalogApi.Products.IRepository;
using catalogApi.Products.Repository;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddTransient<IProductRepository, ProductRepository>();
var app = builder.Build();
app.UseDefaultFiles();   // Looks for index.html automatically
app.UseStaticFiles();
//Configure the HTTP request pipeline
app.MapCarter();
app.Run();
