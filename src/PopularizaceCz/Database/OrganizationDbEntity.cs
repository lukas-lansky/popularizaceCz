using PopularizaceCz.Database.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PopularizaceCz.Database
{
    public class OrganizationDbEntity : IDbEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}