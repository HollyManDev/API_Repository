namespace Holy_Man_API.ModelsView
{
    public class MessageView
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public int idConversation { get; set; }
        public bool status { get; set; }
        public bool seen { get; set; }
        public int UserId { get; set; }
    }
}
