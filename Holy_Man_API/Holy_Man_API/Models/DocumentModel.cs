using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holy_Man_API.Models
{
    public class DocumentModel
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedAt { get; set; }
        public bool? status { get; set; }

        // Chave estrangeira para a Mensagem
        public int? MessageId { get; set; }
        [ForeignKey("MessageId")]
        public virtual UserModel? Message { get; set; }
    }
}
