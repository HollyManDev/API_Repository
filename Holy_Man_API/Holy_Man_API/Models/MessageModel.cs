using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holy_Man_API.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool status { get; set; }

        // Chave estrangeira para a Conversa
        public int ConversationId { get; set; }
        [ForeignKey("ConversationId")]
        public virtual ConversationModel Conversation { get; set; }

        public int UserId { get; set; }

        // Lista de documentos associados à mensagem
        //public virtual ICollection<DocumentModel?> Documents { get; set; } 
    }
}
