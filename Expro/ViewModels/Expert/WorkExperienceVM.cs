﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels.Expert
{
    public class WorkExperienceVM
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        //[Required]
        [Display(Name = "Город")]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Место работы")]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Required]
        [Display(Name = "Должность")]
        [StringLength(256)]
        public string Position { get; set; }

        [Required]
        [Display(Name = "С")]
        public string WorkPeriodFrom { get; set; }

        [Required]
        [Display(Name = "По")]
        public string WorkPeriodTo { get; set; }

        //public WorkExperienceVM(Education model) // : base(model)
        //{
        //    if (model == null)
        //        return;

        //    ID = model.ID;
        //    CountryID = model.ID;
        //    City = model.City;
        //    University = model.University;
        //    Faculty = model.Faculty;
        //    GraduationYear = model.GraduationYear;
        //}
    }
}
