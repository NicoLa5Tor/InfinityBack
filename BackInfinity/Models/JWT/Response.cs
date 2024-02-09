namespace BackInfinity.Models.JWT
{
    public class Response
    {
        public int State { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = string.Empty;
    }
}
