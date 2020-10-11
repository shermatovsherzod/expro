using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Models
{
    public class EducationVM
    {
        public string Id { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Страна")]
        public string CountryID { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Учебное заведение")]
        public string University { get; set; }

        [Display(Name = "Факультет")]
        public string Faculty { get; set; }

        [Display(Name = "Год выпуска")]
        public string GraduationYear { get; set; }
    }
}
