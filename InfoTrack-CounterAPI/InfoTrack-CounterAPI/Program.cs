using InfoTrack_CounterAPI.Data;
using InfoTrack_CounterAPI.Repositories.Implementation;
using InfoTrack_CounterAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(); //added http client

//Binding
builder.Services.AddDbContext<RankDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RankConnString"));
});

// Repositories
builder.Services.AddScoped<ISearchRepository, SearchRepoImplement>(); 
builder.Services.AddScoped<IHistoryRepository, HistoryRepoImplement>(); 

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
