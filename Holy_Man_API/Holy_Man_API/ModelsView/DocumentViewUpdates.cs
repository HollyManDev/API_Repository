namespace Holy_Man_API.ModelsView
{
    public class DocumentViewUpdates
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string UploadedAt { get; set; } 
        public bool Status { get; set; }
        public int UserId { get; set; }
        public int IdConversation { get; set; }
        public bool Downloaded { get; set; }

    }
}
