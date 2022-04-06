using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        ///     Updates user's pseudo
        /// </summary>
        /// <param name="changePseudoParameter"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> ChangePseudo(ChangePseudoParameter changePseudoParameter, Guid userId);

        /// <summary>
        ///     Updates user's password
        /// </summary>
        /// <param name="changePasswordParameter"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> ChangePassword(ChangePasswordParameter changePasswordParameter, Guid userId);

        /// <summary>
        ///     Returns all users by their ids
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<ICollection<User>> GetUsersByIds(ICollection<Guid> userIds);
    }
}
