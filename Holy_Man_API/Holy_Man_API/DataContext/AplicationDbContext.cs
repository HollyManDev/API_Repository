using Microsoft.EntityFrameworkCore;

namespace Holy_Man_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ConversationModel> Conversations { get; set; }
        public DbSet<ConversationParticipantsModel> ConversationParticipants { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Define a chave primária composta para a entidade ConversationParticipant
        //    modelBuilder.Entity<ConversationParticipantsModel>()
        //        .HasKey(cp => new { cp.UserId, cp.ConversationId });

        //    // Define o relacionamento entre ConversationParticipant e UserModel
        //    modelBuilder.Entity<ConversationParticipantsModel>()
        //        .HasOne(cp => cp.User) // Um ConversationParticipant pertence a um UserModel
        //        .WithMany(u => u.ConversationParticipants) // Um UserModel pode ter vários ConversationParticipants
        //        .HasForeignKey(cp => cp.UserId); // Chave estrangeira de ConversationParticipant para UserId

        //    // Define o relacionamento entre ConversationParticipant e ConversationModel
        //    modelBuilder.Entity<ConversationParticipantsModel>()
        //        .HasOne(cp => cp.Conversation) // Um ConversationParticipant pertence a um ConversationModel
        //        .WithMany(c => c.Participants) // Um ConversationModel pode ter vários ConversationParticipants
        //        .HasForeignKey(cp => cp.ConversationId); // Chave estrangeira de ConversationParticipant para ConversationId
        //}
    }
}
