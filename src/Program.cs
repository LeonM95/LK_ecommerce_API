using Microsoft.EntityFrameworkCore;
using test_LK_ecommerce.Data; // Your namespace

var builder = WebApplication.CreateBuilder(args);

// --- Configure Services ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// --- Configure the HTTP request pipeline (Middleware) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();