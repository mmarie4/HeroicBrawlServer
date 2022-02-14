using HeroicBrawlServer.Data.Entities;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class UserResponse : BaseEntityResponse
    {
        public string Token { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Builds a UserResponse from a User entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static UserResponse FromEntity(User entity, string token)
        {
            return new UserResponse()
            {
                Id = entity.Id,
                Token = token,
                Email = entity.Email,
                Pseudo = entity.Pseudo,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy?.Pseudo,
                UpdatedBy = entity.UpdatedBy?.Pseudo,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
