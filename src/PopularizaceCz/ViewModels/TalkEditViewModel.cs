using PopularizaceCz.DataLayer.Entities;
using PopularizaceCz.DataLayer.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PopularizaceCz.ViewModels
{
    public sealed class TalkEditViewModel
    {
        public TalkDbModel DbModel { get; set; }

        public IEnumerable<PersonDbEntity> AllSpeakers { get; set; }

        public IEnumerable<OrganizationDbEntity> AllOrganizations { get; set; }

        public string StartDate
        {
            get
            {
                return DbModel?.Start.ToString("yyyy-MM-dd");
            }

            set
            {
                try
                {
                    DbModel.Start = DateTime.Parse(value);
                }
                catch { }
            }
        }

        public IList<int> SelectedSpeakers
        {
            get
            {
                return DbModel?.Speakers?.Select(s => s.Id)?.ToList() ?? new List<int>();
            }

            set
            {
                if (value != null)
                {
                    DbModel.Speakers = value.Select(id => new PersonDbEntity { Id = id }).ToList();
                }
            }
        }

        public IList<int> SelectedOrganizers
        {
            get
            {
                return DbModel?.Organizers?.Select(o => o.Id)?.ToList() ?? new List<int>();
            }

            set
            {
                if (value != null)
                {
                    DbModel.Organizers = value.Select(id => new OrganizationDbEntity { Id = id }).ToList();
                }
            }
        }
    }
}
