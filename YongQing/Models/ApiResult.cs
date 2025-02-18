namespace YongQing.Models
{
    // 用於攜帶錯誤訊息
    public class ApiResult
    {
        public object? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
