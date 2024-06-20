namespace Holy_Man_API.ServerResponse
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string menssage { get; set; } = string.Empty;
        public bool Success { get; set; } = true;

    }

}
