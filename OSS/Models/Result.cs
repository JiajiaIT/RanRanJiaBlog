namespace OSS.Models
{
    public class Result<T>
    {
        public bool State { get; set; } = true;
        public string Msg { get; set; } = "成功";
        public T Data { get; set; }
    }
}
