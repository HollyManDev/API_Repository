using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Holy_Man_API.Services.ConversationParticipants
{
    public class ConversationParticipantsServices : ConversationParticipantsInterface
    {
        private readonly ApplicationDbContext _context;

        public ConversationParticipantsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ConversationParticipantsModel>>> AddConversationParticipants(ConversationParticipantsView newTalkers)
        {
            var serviceResponse = new ServiceResponse<List<ConversationParticipantsModel>>();

            var participants = new ConversationParticipantsModel();

            try
            {
                if (newTalkers == null)
                {
                    serviceResponse.menssage = "Please provide data for the Participants.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    participants.ConversationId = newTalkers.ConversationId;
                    participants.UserId = newTalkers.UserId;
                    participants.status = true;

                    _context.ConversationParticipants.Add(participants);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.ConversationParticipants.ToListAsync();
                    serviceResponse.Success = true;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error creating conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async  Task<ServiceResponse<List<ConversationParticipantsModel>>> DeleteConversationParticipants(int id)
        {
            var serviceResponse = new ServiceResponse<List<ConversationParticipantsModel>>();

            try
            {
                var participants = await _context.ConversationParticipants.FindAsync(id);

                if (participants == null)
                {
                    serviceResponse.menssage = "Participant not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    participants.status = false;
                    _context.ConversationParticipants.Update(participants);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.ConversationParticipants.ToListAsync();
                    serviceResponse.Success = true;
                }


            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error deleting Participant: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ConversationParticipantsModel>> GetConversationParticipants(int id) 
        {
            var serviceResponse = new ServiceResponse<ConversationParticipantsModel>();

            try
            {
                var conversation = await _context.ConversationParticipants.FindAsync(id);

                if (conversation == null)
                {
                    serviceResponse.menssage = "Participantts not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    serviceResponse.Data = conversation;
                    serviceResponse.Success = true;

                }
              
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving Participants: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> GetConversationId(int userId, int participantId)
        {
            var serviceResponse = new ServiceResponse<int>();

            try
            {
                // Verifica se existe uma conversa que inclui tanto o userId quanto o participantId
                var conversation = await _context.ConversationParticipants
                    .Where(x => x.ConversationId == (from c in _context.ConversationParticipants
                                                     where (c.UserId == userId || c.UserId == participantId)
                                                     group c by c.ConversationId into g
                                                     where g.Count() > 1
                                                     select g.Key).FirstOrDefault())
                    .FirstOrDefaultAsync();

                if (conversation == null)
                {
                    serviceResponse.menssage = "Conversation not found.";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = conversation.ConversationId;
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<List<ConversationParticipantsModel>>> GetConversationsParticipants()
        {
            var serviceResponse = new ServiceResponse<List<ConversationParticipantsModel>>();

            try
            {
                serviceResponse.Data = await _context.ConversationParticipants.ToListAsync();
                serviceResponse.Success = true;

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.menssage = "No Participant found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving Participants: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        //public async Task<ServiceResponse<List<ConversationParticipantsModel>>> UpdateConversationParticipants(ConversationParticipantsView updatedTalkers)
        //{
        //    var serviceResponse = new ServiceResponse<List<ConversationParticipantsModel>>();

        //    try
        //    {
        //        var participant = await _context.ConversationParticipants.FindAsync(updatedTalkers.UserId);

        //        if (participant == null)
        //        {
        //            serviceResponse.menssage = "Partricipant not found.";
        //            serviceResponse.Success = false;
        //            return serviceResponse;
        //        }
        //        else
        //        {
        //            if(participant.ConversationId == updatedTalkers.ConversationId)
        //            {
        //                participant.status = false;
        //                _context.Update(participant);
        //                await _context.SaveChangesAsync();

        //                serviceResponse.Data = await _context.ConversationParticipants.ToListAsync();
        //                serviceResponse.Success = true;
        //            }
                 

        //        }

              
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.menssage = $"Error updating Participant: {ex.Message}";
        //        serviceResponse.Success = false;
        //    }

        //    return serviceResponse;
        //}
    }
}
