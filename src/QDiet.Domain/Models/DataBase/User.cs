using System;
using System.Collections.Generic;
using System.Text;

namespace QDiet.Domain.Models.DataBase
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Refresh токен пользователя
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Дата окончания актуальности Refresh токена
        /// </summary>
        public DateTime RefreshTokenExpireTime { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
