namespace API.Models
{
    public class NotificationRequest
    {
        public string CustomKey { get; set; }
        public string Message { get; set; }
        public string TagKey { get; set; }
        public int TagValue { get; set; }
    }
}
