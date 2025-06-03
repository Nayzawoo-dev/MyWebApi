namespace MyWebApi.Models
{
    public class ResponseModel
    {
        public bool Complete { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

    }
}
