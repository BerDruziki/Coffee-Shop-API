using System.Reflection;
using cafeteria.Controllers;

var builder = WebApplication.CreateBuilder(args);
var corsPolicy = "_corsPolicy";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( x =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    x.IncludeXmlComments(xmlPath);//Implementação de comentários no swagger.
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:corsPolicy,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5142");                                            
                      });
});  

var app = builder.Build();
app.UseRouting();

//Clients
app.MapPost("/registerClient", clientController.registerClient);
app.MapGet("/getClient/{clientId}", clientController.getClient);
app.MapPut("/editClient/{Id}", clientController.editClient);
app.MapDelete("/deleteClient/{Id}", clientController.deleteClient);

//Vendedores
app.MapPost("/registerSeller", sellerController.registerSeller);
app.MapGet("/getSeller/{sellerId}", sellerController.getSeller);
app.MapPut("/editSeller/{Id}", sellerController.editSeller);
app.MapDelete("/deleteSeller/{Id}", sellerController.deleteSeller);

//Café
app.MapPost("/registerCoffee", coffeeController.registerCoffee);
app.MapGet("/getCoffee/{sellerId}", coffeeController.getCoffee);
app.MapPut("/editCoffee/{Id}", coffeeController.editCoffee);
app.MapDelete("/deleteCoffee/{Id}", coffeeController.deleteCoffee);

//Login
app.MapPost("/validatelogin", loginController.validateLogin);

app.UseSwagger();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0.0");
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
