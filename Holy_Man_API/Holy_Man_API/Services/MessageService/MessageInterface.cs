using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;

public interface MessageInterface
{
    Task<ServiceResponse<List<MessageModel>>> GetMessages();
    Task<ServiceResponse<MessageModel>> CreateMessage(MessageView newMessage);
    Task<ServiceResponse<MessageModel>> FindMessage(int id);
    Task<ServiceResponse<MessageModel>> UpdateMessage(MessageView UpdateMessage);
    Task<ServiceResponse<List<MessageModel>>> InactivateMessage(int id);
    Task<ServiceResponse<List<MessageModel>>> ActivateMessage(int id);
    Task<ServiceResponse<List<MessageModel>>> ChangeStatus(MessageView mess); // Atualizado para ChangeStatus
}
