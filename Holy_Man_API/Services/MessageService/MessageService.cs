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
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MessageModel>>> CreateMessage(MessageView newMessage)
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

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
                        SentAt = DateTime.Now.ToLocalTime(),
                        ConversationId = newMessage.idConversation,
                        status = newMessage.status,
                        UserId = newMessage.UserId
                    };

                    _context.Messages.Add(messageModel);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Messages.ToListAsync();
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
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MessageModel>>> UpdateMessage(MessageView updateMessage)
        {
            var serviceResponse = new ServiceResponse<List<MessageModel>>();

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
                    gotMessage.SentAt = DateTime.Now.ToLocalTime();
                    gotMessage.status = updateMessage.status;

                    _context.Messages.Update(gotMessage);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Messages.ToListAsync();
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
