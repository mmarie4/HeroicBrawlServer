using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Shared.Settings
{
    public class HeroSettings
    {
        public Guid Id { get; }
        public int BaseHp { get; }

        public HeroSettings(Guid guid, int baseHp)
        {
            Id = guid;
            BaseHp = baseHp;
        }
    }

    public class HeroesSettings
    {
        public ICollection<HeroSettings> Heroes = new List<HeroSettings>()
        {
            new HeroSettings(new Guid("03fa247c-7c24-453d-8efa-ff0a0e605279"), 1000)
        };

        public HeroSettings Get(Guid id) => Heroes.First(h => h.Id == id);
    }
}
