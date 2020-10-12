using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models
{
    public class Education : BaseModelAuthorable
    {
        public int CountryID { get; set; }
        public int? CityID { get; set; }
        public string CityOther { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public int GraduationYear { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}
