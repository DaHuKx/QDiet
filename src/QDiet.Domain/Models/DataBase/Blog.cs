using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDiet.Domain.Models.DataBase
{
    /// <summary>
    /// Блог
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Айди
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id владельца блога
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        /// Название блога
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Владелец
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Посты блога
        /// </summary>
        public List<Post> Posts { get; set; }

        /// <summary>
        /// Подписчики блога
        /// </summary>
        public List<User> Subscribers { get; set; }
    }
}
