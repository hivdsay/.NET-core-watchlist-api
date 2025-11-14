using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WatchList.Business.Abstract.App;
using WatchList.Business.Concrete.Containers;
using WatchList.Core.Tools.Concrete.Mapper;
using WatchList.Business.Abstract.Generic;
using WatchList.Business.Concrete.App;
using WatchList.Business.Concrete.Generic;
using WatchList.Core.Tools.Concrete.JwtTool;
using WatchList.DataAccess.Concrete.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(ProjectMapper));
builder.Services.AddDependencies(builder.Configuration);

builder.Services.Configure<JwtInfo>(builder.Configuration.GetSection("JWTInfo"));
var jwtInfo = builder.Configuration.GetSection("JWTInfo").Get<JwtInfo>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtInfo.Issuer,
        ValidAudience = jwtInfo.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecurityKey)),
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero
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
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

