﻿using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.API.Models.Users
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     Builds a SignUpParameter from a SignUpRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SignUpParameter ToParameter(SignUpRequest request)
        {
            return new SignUpParameter()
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}