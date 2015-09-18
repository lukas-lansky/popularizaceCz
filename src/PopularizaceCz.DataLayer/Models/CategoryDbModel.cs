using PopularizaceCz.DataLayer.Entities;
using System.Collections.Generic;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.DataLayer.Models
{
    public class CategoryDbModel : CategoryDbEntity
    {
        public CategoryDbModel(CategoryDbEntity entity, IList<CategoryDbEntity> supCats, IList<CategoryDbEntity> subCats, IList<TalkDbEntity> talks)
        {
            entity.MapTo(this);

            this.DirectSupCategories = supCats;

            this.DirectSubCategories = subCats;

            this.Talks = talks;
        }

        public IList<CategoryDbEntity> DirectSupCategories { get; set; }

        public IList<CategoryDbEntity> DirectSubCategories { get; set; }

        public IList<TalkDbEntity> Talks { get; set; }
    }
}