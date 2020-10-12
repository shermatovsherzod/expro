using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Education : BaseModelAuthorable
    {
        [ForeignKey("Country")]
        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }
        public string CityOther { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public int GraduationYear { get; set; }
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}
