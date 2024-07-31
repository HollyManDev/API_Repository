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
                // Consulta para obter a conversa entre userId e participantId
                var conversation = await (from conv in _context.Conversations
                                          join cp1 in _context.ConversationParticipants
                                              on conv.Id equals cp1.ConversationId
                                          join cp2 in _context.ConversationParticipants
                                              on conv.Id equals cp2.ConversationId
                                          where cp1.UserId == userId
                                              && cp2.UserId == participantId
                                              && conv.status == true && conv.type != "group"
                                          select conv)
                                          .FirstOrDefaultAsync();

                if (conversation == null)
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
                    ConversationId = conversation.Id, // Adicionando o ID da conversa
                    Messages = await _context.Messages
                                        .Where(m => m.ConversationId == conversation.Id)
                                        .ToListAsync(),
                    Documents = await _context.Documents
                                        .Where(d => d.ConversationId == conversation.Id)
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
        public async Task<ServiceResponse<UserContent>> GetGroupContent(int conversationId)
        {
            var serviceResponse = new ServiceResponse<UserContent>();

            try
            {
                // Consultar a conversa com o ID fornecido
                var conversation = await _context.Conversations
                                                 .Where(conv => conv.Id == conversationId)
                                                 .FirstOrDefaultAsync();

                if (conversation == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                // Obter mensagens para a conversa
                var messages = await _context.Messages
                                             .Where(m => m.ConversationId == conversationId)
                                             .ToListAsync();

                // Obter documentos para a conversa
                var documents = await _context.Documents
                                              .Where(d => d.ConversationId == conversationId)
                                              .ToListAsync();

                // Criar UserContent com os dados da conversa
                var userContent = new UserContent
                {
                    UserId = 0, // Este campo não será utilizado, mas mantido para compatibilidade
                    ParticipantId = 0, // Este campo não será utilizado, mas mantido para compatibilidade
                    ConversationId = conversationId,
                    Messages = messages,
                    Documents = documents
                };

                // Configurar o ServiceResponse
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
