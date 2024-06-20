using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;

namespace Holy_Man_API.Services.ConversationService
{
    public class ConversationService: ConversationInterface
    {
        private readonly ApplicationDbContext _context;

        public ConversationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ConversationModel>>> GetConversations()
        {
            var serviceResponse = new ServiceResponse<List<ConversationModel>>();

            try
            {
                serviceResponse.Data = await _context.Conversations.ToListAsync();
                serviceResponse.Success = true;

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.menssage = "No conversations found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving conversations: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ConversationModel>> GetConversation(int id)
        {
            var serviceResponse = new ServiceResponse<ConversationModel>();

            try
            {
                var conversation = await _context.Conversations.FindAsync(id);

                if (conversation == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                serviceResponse.Data = conversation;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConversationModel>>> CreateConversation(ConversationView newConversation)
        {
            var serviceResponse = new ServiceResponse<List<ConversationModel>>();
            var conversation = new ConversationModel();

            try
            {
                if (newConversation == null)
                {
                    serviceResponse.menssage = "Please provide data for the conversation.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

              

                conversation.Title = newConversation.Title;
                conversation.CreatedAt = DateTime.Now;
                conversation.status = true;
                  
                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Conversations.ToListAsync();
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error creating conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConversationModel>>> UpdateConversation(ConversationView updatedConversation)
        {
            var serviceResponse = new ServiceResponse<List<ConversationModel>>();

            try
            {
                var conversation = await _context.Conversations.FindAsync(updatedConversation.Id);

                if (conversation == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

                conversation.Title = updatedConversation.Title;
                conversation.CreatedAt = DateTime.Now;

                _context.Update(conversation);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Conversations.ToListAsync();
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error updating conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ConversationModel>>> DeleteConversation(int id)
        {
            var serviceResponse = new ServiceResponse<List<ConversationModel>>();

            try
            {
                var conversation = await _context.Conversations.FindAsync(id);

                if (conversation == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    conversation.status = false;
                    _context.Conversations.Update(conversation);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.Conversations.ToListAsync();
                    serviceResponse.Success = true;
                }

                
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error deleting conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
