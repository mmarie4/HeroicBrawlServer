using HeroicBrawlServer.Services.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace HeroicBrawlServer.Controllers.Models.Users
{
    public class SignUpRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }

        /// <summary>
        ///     Builds a SignUpParameter from a SignUpRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SignUpParameter ToParameter(SignUpRequest request)
        {
            return new SignUpParameter()
            {
                Email = request.Email.Trim(),
                Pseudo = request.Pseudo,
                Password = request.Password
            };
        }

        /// <summary>
        ///     Validates request
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            var email = new MailAddress(Email.Trim());
            if (string.IsNullOrWhiteSpace(email.Address))
            {
                throw new Exception("Invalid email format");
            }

            if (Password != Password2)
            {
                throw new Exception("Passwords don't match");
            }
        }
    }
}
