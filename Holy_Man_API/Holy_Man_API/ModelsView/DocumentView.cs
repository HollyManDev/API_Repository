namespace Holy_Man_API.ModelsView
{
    public class DocumentView
    {
        public IFormFile File { get; set; }  // Propriedade para receber o arquivo no upload
        public int ConversationId { get; set; }
        public bool doawloaded { get; set; }    
        public int UserId { get; set; }
    }
}
