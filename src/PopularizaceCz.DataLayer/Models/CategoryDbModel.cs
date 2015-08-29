using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;

namespace PopularizaceCz.DataLayer.Models
{
    public class CategoryDbModel
    {
        public CategoryDbEntity Entity { get; set; }

        public IList<CategoryDbEntity> DirectSupCategories { get; set; }

        public IList<CategoryDbEntity> DirectSubCategories { get; set; }

        public IList<TalkDbEntity> Talks { get; set; }
    }
}