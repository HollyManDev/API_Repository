using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holy_Man_API.Models
{
    public class ConversationParticipantsModel
    {
        [Key]
        public int id { get; set; }
        public int  UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserModel? User { get; set; }

        public int? ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public virtual ConversationModel? Conversation { get; set; }
        public bool  status { get; set; }
    }
}
