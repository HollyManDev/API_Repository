using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holy_Man_API.Models
{
    public class DocumentModel
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool status { get; set; }
        public bool doawloaded { get; set; }
        public int ConversationId { get; set; }
        [ForeignKey("ConversationId")]
     
        public virtual ConversationModel Conversation { get; set; }
        public int UserId { get; set; }


    }
}
