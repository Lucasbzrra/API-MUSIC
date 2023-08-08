using API_MUSIC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

    var MusicConnectionstring = builder.Configuration.GetConnectionString("MusicConncetion");
    var ArtistaConncetionstring = builder.Configuration.GetConnectionString("artistconncetion");


    builder.Services.AddDbContext<MusicContext>(opts => opts.UseMySql(MusicConnectionstring, ServerVersion.AutoDetect(MusicConnectionstring)));
    builder.Services.AddDbContext<ArtistContext>(opts => opts.UseMySql(ArtistaConncetionstring, ServerVersion.AutoDetect(ArtistaConncetionstring)));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



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
