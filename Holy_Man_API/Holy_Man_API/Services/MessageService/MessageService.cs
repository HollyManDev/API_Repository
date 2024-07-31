using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;

namespace Holy_Man_API.Services.MessageService
{
    public class MessageService : MessageInterface
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<MessageModel>>> ActivateMessage(int id)
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

            try
            {
                var gotMessage = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);

                if (gotMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotMessage.status = true;
                    _context.Messages.Update(gotMessage);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Messages.ToListAsync();
                    serviceResponse.menssage = "Message activated successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MessageModel>>> ChangeStatus(MessageView mess)
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

            try
            {
                var messages = await _context.Messages
                    .Where(m => m.ConversationId == mess.idConversation)
                    .ToListAsync();

                if (messages == null || !messages.Any())
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "No messages found for the specified conversation!";
                    serviceResponse.Success = false;
                }
                else
                {
                    foreach (var message in messages)
                    {
                        if(message.UserId == mess.UserId) 
                        {
                            message.seen = true;
                        }
                       
                    }

                    _context.Messages.UpdateRange(messages);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = messages;
                    serviceResponse.menssage = "Messages updated successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageModel>> CreateMessage(MessageView newMessage)
        {
            var serviceResponse = new ServiceResponse<MessageModel>();

            try
            {
                if (newMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Introduce data!";
                    serviceResponse.Success = false;
                }
                else
                {
                    var messageModel = new MessageModel
                    {
                        Content = newMessage.Content,
                        SentAt = DateTime.UtcNow,
                        ConversationId = newMessage.idConversation,
                        status = newMessage.status,
                        UserId = newMessage.UserId,
                        seen = false
                    };

                    _context.Messages.Add(messageModel);
                    await _context.SaveChangesAsync();

                    var lastMessage = await _context.Messages
                        .OrderByDescending(m => m.Id)
                        .FirstOrDefaultAsync();

                    serviceResponse.Data = lastMessage;
                    serviceResponse.menssage = "Message created successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageModel>> FindMessage(int id)
        {
            var serviceResponse = new ServiceResponse<MessageModel>();

            try
            {
                var gotMessage = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);

                if (gotMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = gotMessage;
                    serviceResponse.menssage = "Message found successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MessageModel>>> GetMessages()
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

            try
            {
                serviceResponse.Data = await _context.Messages.ToListAsync();

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.menssage = "No data available!";
                }
                else
                {
                    serviceResponse.menssage = "Messages retrieved successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MessageModel>>> InactivateMessage(int id)
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

            try
            {
                var gotMessage = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);

                if (gotMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotMessage.status = false;
                    _context.Messages.Update(gotMessage);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Messages.ToListAsync();
                    serviceResponse.menssage = "Message inactivated successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<MessageModel>> UpdateMessage(MessageView updateMessage)
        {
            var serviceResponse = new ServiceResponse<MessageModel>();

            try
            {
                var gotMessage = await _context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == updateMessage.Id);

                if (gotMessage == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotMessage.Content = updateMessage.Content;
                    gotMessage.SentAt = DateTime.UtcNow;
                    gotMessage.status = updateMessage.status;
                    gotMessage.seen = false;

                    _context.Messages.Update(gotMessage);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = gotMessage;
                    serviceResponse.menssage = "Message updated successfully!";
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
