using System;
using HeroicBrawlServer.DAL.Entities;

namespace HeroicBrawlServer.API.Models
{
    public class BaseEntityResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
