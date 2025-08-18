using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using src.Data;
using src.Services;
using System.Text;
using test_LK_ecommerce.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --- Configure Services ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReviewService, ReviewService>();




// Add Swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add and configure JWT Authentication
//builder.services.addauthentication(jwtbearerdefaults.authenticationscheme)
//    .addjwtbearer(options =>
//    {
//        options.tokenvalidationparameters = new tokenvalidationparameters
//        {
//            validateissuer = true,
//            validateaudience = true,
//            validatelifetime = true,
//            validateissuersigningkey = true,
//            validissuer = builder.configuration["jwtsettings:issuer"],
//            validaudience = builder.configuration["jwtsettings:audience"],
//            issuersigningkey = new symmetricsecuritykey(encoding.utf8.getbytes(builder.configuration["jwtsettings:secretkey"]!))
//        };
//    });

var app = builder.Build();



// --- Configure the HTTP request pipeline (Middleware) ---

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication must come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();