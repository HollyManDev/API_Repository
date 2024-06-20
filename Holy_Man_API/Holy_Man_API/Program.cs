
using Holy_Man_API.Models;
using Holy_Man_API.Services.ConversationParticipants;
using Holy_Man_API.Services.ConversationService;
using Holy_Man_API.Services.DocumentService;
using Holy_Man_API.Services.MessageService;
using Holy_Man_API.Services.UserServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserInterface, UserService>();
builder.Services.AddScoped<MessageInterface, MessageService>();
builder.Services.AddScoped<DocumentInterface, DocumentService>();
builder.Services.AddScoped<ConversationInterface, ConversationService>();
builder.Services.AddScoped<ConversationParticipantsInterface, ConversationParticipantsServices>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
