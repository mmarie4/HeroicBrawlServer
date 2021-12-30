namespace HeroicBrawlServer.Services.Models.Messages.PlayerActions
{
    public class TakeDamageMessage : BasePlayerActionMessage
    {
        public int DamageTaken { get; set; }
    }
}
