﻿using System;
using System.Threading.Tasks;
using HeroicBrawlServer.DAL.Entities;
using HeroicBrawlServer.Services.Models.Users;

namespace HeroicBrawlServer.Services.Abstractions
{
    public interface IUserService
    {
        /// <summary>
        ///     Logs in a user
        /// </summary>
        /// <param name="loginParameter">Login parameter</param>
        /// <returns></returns>
        Task<(User, string)> Login(LoginParameter loginParameter);

        /// <summary>
        ///     Sign up a user
        /// </summary>
        /// <param name="signUpParameter">Sign up parameter</param>
        /// <returns></returns>
        Task<(User, string)> SignUpAsync(SignUpParameter signUpParameter);

        /// <summary>
        ///     Returns user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns></returns>
        Task<User> GetByIdAsync(Guid userId);
    }
}