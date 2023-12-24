using System;
using System.Collections.Generic;
using System.Text;

namespace QDiet.Domain.Models.DataBase
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Айди
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пользователи с данной ролью
        /// </summary>
        public List<User> Users { get; set; }
    }
}
