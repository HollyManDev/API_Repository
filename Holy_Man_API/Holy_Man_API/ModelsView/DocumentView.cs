namespace Holy_Man_API.ModelsView
{
    public class DocumentView
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool status { get; set; }
    }
}
