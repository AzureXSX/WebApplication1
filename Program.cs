using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebApplication1;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("todoAppDBCon")));
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["sp=racwdli&st=2023-10-25T23:36:08Z&se=2023-10-28T07:36:08Z&spr=https&sv=2022-11-02&sr=c&sig=015GBO0Ib1f8PagzUtke0qZNXPZXhaQk45Tjxu1O9Fw%3D:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["sp=racwdli&st=2023-10-25T23:36:08Z&se=2023-10-28T07:36:08Z&spr=https&sv=2022-11-02&sr=c&sig=015GBO0Ib1f8PagzUtke0qZNXPZXhaQk45Tjxu1O9Fw%3D:queue"], preferMsi: true);
});

var app = builder.Build();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
