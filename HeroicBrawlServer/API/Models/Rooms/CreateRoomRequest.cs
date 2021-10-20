using HeroicBrawlServer.Services.Models.Rooms;

namespace HeroicBrawlServer.API.Models.Rooms
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public int Max { get; set; }


        /// <summary>
        /// Builds a CreateRoomParameter from a CreateRoomRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateRoomParameter ToParameter(CreateRoomRequest request)
        {
            return new CreateRoomParameter()
            {
                Name = request.Name,
                Max = request.Max
            };
        }
    }
}
