namespace HeroicBrawlServer.Controllers.Models.Rooms
{
    public class SearchRoomRequest
    {
        public string? SearchTerm { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
