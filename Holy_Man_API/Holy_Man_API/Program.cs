using Holy_Man_API.Models;
using Holy_Man_API.Services.All_User_Participants_Conversations;
using Holy_Man_API.Services.ConversationParticipants;
using Holy_Man_API.Services.ConversationService;
using Holy_Man_API.Services.DepartmentService;
using Holy_Man_API.Services.DocumentService;
using Holy_Man_API.Services.MessageService;
using Holy_Man_API.Services.UserContentService;
using Holy_Man_API.Services.UserContentServices;
using Holy_Man_API.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignorar ciclos de referências
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserInterface, UserService>();
builder.Services.AddScoped<MessageInterface, MessageService>();
builder.Services.AddScoped<DocumentInterface, DocumentService>();
builder.Services.AddScoped<ConversationInterface, ConversationService>();
builder.Services.AddScoped<ConversationParticipantsInterface, ConversationParticipantsServices>();
builder.Services.AddScoped<UserContentInterfacecs, UserContentService>();
builder.Services.AddScoped<All_User_Conversation_Participnts_Services>();
builder.Services.AddScoped<All_User_Conversation_Participnts_Interface, All_User_Conversation_Participnts_Services>();
builder.Services.AddScoped<DepartmentInterface, DepartmentService>();
// Configuração do DbContext para o Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar o middleware de CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
