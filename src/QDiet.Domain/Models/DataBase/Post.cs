using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QDiet.Domain.Models.DataBase
{
    /// <summary>
    /// Пост
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Айди
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id блога, которому принадлежит пост
        /// </summary>
        public long BlogId { get; set; }

        /// <summary>
        /// Название поста
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Блог
        /// </summary>
        public Blog Blog { get; set; }
    }
}