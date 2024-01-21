using APIChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
{
    cors.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials().WithOrigins("http://localhost:8080");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<HubProvider>("/hub");

app.Run();