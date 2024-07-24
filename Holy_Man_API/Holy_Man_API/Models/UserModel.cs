using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Acess { get; set; }
        public int DepartmentId { get; set; }  // Nova propriedade para a chave estrangeira

        [ForeignKey("DepartmentId")]  // Indica que DepartmentId é uma chave estrangeira
        public virtual DepartmentModel Department { get; set; }  // Propriedade de navegação para o departamento

        // Lista de mensagens associadas ao usuário
        public virtual ICollection<MessageModel?> Messages { get; set; }

        // Lista de conversas em que o usuário participa
        public virtual ICollection<ConversationParticipantsModel?> ConversationParticipants { get; set; }
    }
}
