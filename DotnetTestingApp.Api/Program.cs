using DomainBusinesses.Repositories.Users;
using DotnetTestingApp.Entities.DB;
using Services.PasswordHasher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService>();

builder.Services
    .AddFluentEmail(builder.Configuration["Email:SenderEmail"], builder.Configuration["Email:Sender"])
    .AddSmtpSender(builder.Configuration["Email:Host"], builder.Configuration.GetValue<int>("Email:Port"));

builder.Services.AddScoped<RegisterUser>();
//builder.Services.AddScoped<LoginUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var client = new DatabaseContext(builder.Configuration))
{
    client.Database.EnsureCreated();
}

app.Run();
