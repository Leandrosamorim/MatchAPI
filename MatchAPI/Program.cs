using Domain.DeveloperNS.HttpService;
using Domain.DeveloperNS.Interfaces;
using Domain.OrganizationNS.HttpService;
using Domain.OrganizationNS.Interfaces;
using IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpClient<IOrganizationHttpService, OrganizationHttpService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("OrganizationAPI"));
    });
builder.Services.AddHttpClient<IDeveloperHttpService, DeveloperHttpService>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("DeveloperAPI"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("OrganizationAPI", b => b.WithOrigins(builder.Configuration.GetValue<string>("OrganizationAPI")).AllowAnyHeader().AllowAnyMethod());
    options.AddPolicy("DeveloperAPI", b => b.WithOrigins(builder.Configuration.GetValue<string>("DeveloperAPI")).AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
