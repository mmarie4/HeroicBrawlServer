using System;
using System.Collections.Generic;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.Shared.Models;

namespace HeroicBrawlServer.API.Models.Rooms
{
    public class RoomResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int Max { get; set; }


        /// <summary>
        /// Builds a RoomResponse from a Room entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static RoomResponse FromEntity(Room entity)
        {
            return new RoomResponse()
            {
                Id = entity.Id,
                Max = entity.Max,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                UpdatedAt = entity.UpdatedAt
            };
        }

        /// <summary>
        /// Builds RoomResponse for each item of a Room entities collection
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static PaginatedList<RoomResponse> FromEntities(PaginatedList<Room> entities)
        {
            var result = new PaginatedList<RoomResponse>();
            foreach(var entity in entities.Values)
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
