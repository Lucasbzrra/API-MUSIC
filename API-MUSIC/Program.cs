using API_MUSIC.Authorization;
using API_MUSIC.Data;
using API_MUSIC.Data.EfCore;
using API_MUSIC.Models;
using API_MUSIC.Services;
using API_MUSIC.Services.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var MusicConnectionstring = builder.Configuration.GetConnectionString("MusicConncetion");
var UsersConnectionString = builder.Configuration.GetConnectionString("UsersConnection");
//var ArtistaConncetionstring = builder.Configuration.GetConnectionString("artistconncetion");
//builder.Services.AddDbContext<IMusicContext>(opts => opts.UseMySql(MusicConnectionstring, ServerVersion.AutoDetect(MusicConnectionstring)));    

//.UseLazyLoadingProxies FAzer o carregamento dos relacionamento de entidades

// builder.Services.AddDbContext<IMusicContext>(opts => opts.UseLazyLoadingProxies().UseMySql(MusicConnectionstring, ServerVersion.AutoDetect(MusicConnectionstring)));
 builder.Services.AddDbContext<UsersDbContext>(opts => opts.UseLazyLoadingProxies().UseMySql(UsersConnectionString, ServerVersion.AutoDetect(UsersConnectionString)));
//builder.Services.AddDbContext<UsersDbContext>(opts => opts.UseMySql(UsersConnectionString, ServerVersion.AutoDetect(UsersConnectionString)));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<ArtistDaoComEfCore>();
    builder.Services.AddScoped<MusicDaoComEfCore>();
    builder.Services.AddScoped<AddressDaoComEfCore>();
    builder.Services.AddScoped<AdminDaoComEfCore>();

    // O asp net passando o parâmetro para o construtor na linha 18 Da Controle Music -DIP
    builder.Services.AddTransient<IMusicDao,MusicDaoComEfCore>();
    // O asp net passando o parâmetro para o construtor na linha 15 Da Controle Artist -DIP
    builder.Services.AddTransient<IArtistDaocs, ArtistDaoComEfCore>();
    // O asp net passando o parâmetro para o construtor na linha 16 Da Controle Artist -OCP
    builder.Services.AddTransient<IArtistServices,DefaultArtistService>();
    // O asp net passando o parâmetro para o construtor na linha 18 Da Controle Artist -OCP
    builder.Services.AddTransient<IProductService, DefaultAdminProduct>();

    builder.Services.AddTransient<IAddressDao, AddressDaoComEfCore>();

    builder.Services.AddTransient<IRegistrationAddress, RegistrationAddress>();

    builder.Services.AddTransient<IAdminService, DefaultAdmin>();
    builder.Services.AddTransient<IAdminDao, AdminDaoComEfCore>();


builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UsersDbContext>().AddDefaultTokenProviders();
///
//builder.Services.AddIdentity<Users, IdentityRole>().AddEntityFrameworkStores<UsersDbContext>().AddDefaultTokenProviders();
// (https://learn.microsoft.com/pt-br/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-6.0)
//builder.Services.AddIdentity<Users,IdentityRole<int>>()
//           .AddEntityFrameworkStores<UsersDbContext>()
//           .AddDefaultTokenProviders();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddTransient<IAuthorizationHandler, AuthorizationAdmin>();

///
builder.Services.AddAuthorization(opt => opt.AddPolicy("ApenasAdmin", policy => policy.AddRequirements(new ApenasAdmin(true))));


builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_MUSIC", Version = "v1" });
    var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sjdmakaaaasdlapkdaopkdiajnfmnacswa")),
        ValidateAudience =false,
        ValidateIssuer = false,
        ClockSkew=TimeSpan.Zero

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();