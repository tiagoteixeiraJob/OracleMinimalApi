using Microsoft.EntityFrameworkCore;
using OracleMinimalAPI.Config;
using OracleMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSwagger();


app.MapPost("AddProduct", async (Product product, DataContext dataContext) =>
{
    dataContext.Products.Add(product);
    await dataContext.SaveChangesAsync();
});

app.MapDelete("DeleteProduct/{id}", async (int id, DataContext dataContext) =>
{
    var product = await dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

    if(product!= null)
    {
        dataContext.Products.Remove(product);
        await dataContext.SaveChangesAsync();
    }
});

app.MapGet("ListProducts", async (DataContext dataContext) =>
{
    return await dataContext.Products.ToListAsync();
});

app.MapGet("ListProducts/{id}", async (int id, DataContext dataContext) =>
{
    var product = await dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
});

app.UseSwaggerUI();

app.Run();
