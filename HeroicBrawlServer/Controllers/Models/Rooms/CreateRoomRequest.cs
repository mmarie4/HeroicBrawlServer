using HeroicBrawlServer.Services.Models.Maps;
using HeroicBrawlServer.Services.Models.Rooms;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeroicBrawlServer.Controllers.Models.Rooms
{
    public class CreateRoomRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Max { get; set; }
        [Required]
        public Map Map { get; set; }


        /// <summary>
        ///     Builds a CreateRoomParameter from a CreateRoomRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateRoomParameter ToParameter(CreateRoomRequest request)
        {
            return new CreateRoomParameter()
            {
                Name = request.Name,
                Max = request.Max,
                Map = request.Map
            };
        }
    }
}
