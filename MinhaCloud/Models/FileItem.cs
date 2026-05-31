namespace MinhaCloud.Models
{
    public class FileItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string OriginalName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long SizeBytes { get; set; }
        public string StoragePath { get; set; } = string.Empty;
        public string? ShareToken { get; set; }
        public DateTime? ShareExpiresAt {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
    }
}
