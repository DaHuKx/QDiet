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
        public DateTime RefreshTokenExpireEndDate { get; set; }

        /// <summary>
        /// Дата последней активности
        /// </summary>
        public DateTime LastActivityAt { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public List<Role> Roles { get; set; }

        /// <summary>
        /// Блог пользователя
        /// </summary>
        public Blog Blog { get; set; }

        /// <summary>
        /// Блоги, на которые пользователь подписан
        /// </summary>
        public List<Blog> Blogs { get; set; }
    }
}
