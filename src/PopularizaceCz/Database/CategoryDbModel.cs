using Dapper;
using PopularizaceCz.Database.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PopularizaceCz.Database
{
    public class CategoryDbModel
    {
        public CategoryDbEntity Entity { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectSupCategories { get; set; }

        public IReadOnlyCollection<CategoryDbEntity> DirectSubCategories { get; set; }

        public IReadOnlyCollection<TalkDbEntity> Talks { get; set; }
    }
}