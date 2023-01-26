namespace Weather.Core.Models
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }
        
        public DateTime ExpiryTime { get; set; }
        public DateTime GeneratedTime { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}