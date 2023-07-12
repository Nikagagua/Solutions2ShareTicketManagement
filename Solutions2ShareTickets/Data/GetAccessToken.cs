namespace Solutions2ShareTickets.Data
{
    public partial class GetAccessToken
    {
        private int id { get; set; }
        
        public required string? AccessToken { get; set; }

        public required string? CreatedAt { get; set; }

        
    }
}