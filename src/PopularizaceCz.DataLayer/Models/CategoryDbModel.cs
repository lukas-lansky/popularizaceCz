using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;

namespace PopularizaceCz.DataLayer.Models
{
    public class CategoryDbModel
    {
        public CategoryDbEntity Entity { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectSupCategories { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectSubCategories { get; set; }

        public IReadOnlyCollection<TalkDbEntity> Talks { get; set; }
    }
}