using API_MUSIC.Data;
using API_MUSIC.Data.EfCore;
using API_MUSIC.Services;
using API_MUSIC.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MusicConnectionstring = builder.Configuration.GetConnectionString("MusicConncetion");
//var ArtistaConncetionstring = builder.Configuration.GetConnectionString("artistconncetion");
//builder.Services.AddDbContext<IMusicContext>(opts => opts.UseMySql(MusicConnectionstring, ServerVersion.AutoDetect(MusicConnectionstring)));    

//.UseLazyLoadingProxies FAzer o carregamento dos relacionamento de entidades

    builder.Services.AddDbContext<IMusicContext>(opts => opts.UseLazyLoadingProxies().UseMySql(MusicConnectionstring, ServerVersion.AutoDetect(MusicConnectionstring)));

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<ArtistDaoComEfCore>();
    builder.Services.AddScoped<MusicDaoComEfCore>();
    builder.Services.AddScoped<AddressDaoComEfCore>();
    // O asp net passando o parâmetro para o construtor na linha 18 Da Controle Music -DIP
    builder.Services.AddTransient<IMusicDao,MusicDaoComEfCore>();
    // O asp net passando o parâmetro para o construtor na linha 15 Da Controle Artist -DIP
    builder.Services.AddTransient<IArtistDaocs, ArtistDaoComEfCore>();
    // O asp net passando o parâmetro para o construtor na linha 16 Da Controle Artist -OCP
    builder.Services.AddTransient<IAdminServices,DefaultAdminService>();
    // O asp net passando o parâmetro para o construtor na linha 18 Da Controle Artist -OCP
    builder.Services.AddTransient<IProductService, DefaultAdminProduct>();

    builder.Services.AddTransient<IAddressDao, AddressDaoComEfCore>();

    builder.Services.AddTransient<IRegistrationAdmin, RegistrationAdmin>();



builder.Services.AddControllers().AddNewtonsoftJson();//opt=>opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_MUSIC", Version = "v1" });
    var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
    c.IncludeXmlComments(xmlPath);
});

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

app.Run();
