namespace ProductManagementApi.Response
{
    public class ResponseModel<T>
    {
        public bool Status { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

    }
}
