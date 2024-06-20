using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holy_Man_API.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }

        // Lista de mensagens associadas ao usuário
        public virtual ICollection<UserModel?> Messages { get; set; }

        // Lista de conversas em que o usuário participa
        public virtual ICollection<ConversationParticipantsModel?> ConversationParticipants { get; set; }
    }
}
