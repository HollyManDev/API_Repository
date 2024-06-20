using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holy_Man_API.Models
{
    public class ConversationModel
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; } // Nome opcional da conversa
        public DateTime? CreatedAt { get; set; }
        public bool?  status { get; set; }

        // Lista de mensagens na conversa
        public virtual ICollection<UserModel?> Messages { get; set; }

        // Lista de participantes da conversa
        public virtual ICollection<ConversationParticipantsModel?> Participants { get; set; }
    }
}
