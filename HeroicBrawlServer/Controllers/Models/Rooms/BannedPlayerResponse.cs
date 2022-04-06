using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Shared.Models;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class BannedPlayerResponse : BaseEntityResponse
    {
        public string Pseudo { get; set; }

        /// <summary>
        /// Builds a UserResponse from a User entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static BannedPlayerResponse FromEntity(User entity)
        {
            return new BannedPlayerResponse()
            {
                Id = entity.Id,
                Pseudo = entity.Pseudo,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy?.Pseudo,
                UpdatedBy = entity.UpdatedBy?.Pseudo,
                UpdatedAt = entity.UpdatedAt
            };
        }

        /// <summary>
        ///     Builds UserResponse for each item of a User entities collection
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static PaginatedList<BannedPlayerResponse> FromEntities(PaginatedList<User> entities)
        {
            var result = new PaginatedList<BannedPlayerResponse>();
            foreach (var entity in entities.Values)
            {
                result.Values.Add(FromEntity(entity));
            }

            result.Limit = entities.Limit;
            result.Offset = entities.Offset;
            result.Total = entities.Total;

            return result;
        }
    }
}
