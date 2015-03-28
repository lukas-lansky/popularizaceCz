using PopularizaceCz.Database;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PopularizaceCz.ViewModels
{
    public sealed class PersonViewModel
    {
        public PersonDbModel DbModel { get; set; }
    }
}
