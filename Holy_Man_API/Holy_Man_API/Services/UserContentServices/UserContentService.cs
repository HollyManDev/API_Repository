using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.UserContentServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Holy_Man_API.Services.UserContentService
{
    public class UserContentService : UserContentInterfacecs
    {
        private readonly ApplicationDbContext _context;

        public UserContentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<UserContent>> GetConversationContent(int userId, int participantId)
        {
            var serviceResponse = new ServiceResponse<UserContent>();

            try
            {
                // Obter a conversa entre userId e participantId
                var conversationParticipant = await (from conversationBetweenUsers in _context.ConversationParticipants
                                                     where conversationBetweenUsers.status == true
                                                     join participant in _context.ConversationParticipants on
                                                     conversationBetweenUsers.id equals participant.ConversationId
                                                     join user in _context.Users on participant.UserId equals user.Id select conversationBetweenUsers.ConversationId).ToListAsync();
                    

                                              
                if (conversationParticipant == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

               

                // Agora buscar as mensagens e documentos relacionados à conversa
                var userContent = new UserContent
                {
                    UserId = userId,
                    ParticipantId = participantId,
                    ConversationId = conversationParticipant, // Adicionando o ID da conversa
                    Messages = await _context.Messages
                                        .Where(m => m.ConversationId == conversationId)
                                        .ToListAsync(),
                    Documents = await _context.Documents
                                        .Where(d => d.ConversationId == conversationId)
                                        .ToListAsync()
                    // Adicione mais lógica conforme necessário para recuperar outros dados da conversa
                };

                serviceResponse.Data = userContent;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving conversation content: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
