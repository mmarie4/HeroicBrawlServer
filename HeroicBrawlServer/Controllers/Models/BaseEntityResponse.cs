﻿using System;

namespace HeroicBrawlServer.Controllers.Models
{
    public class BaseEntityResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
