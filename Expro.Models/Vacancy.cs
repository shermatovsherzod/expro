using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Vacancy : BaseModelAuthorable
    {
        [Required]
        [StringLength(400)]
        public string Company { get; set; }

        [ForeignKey("Region")]
        public int? RegionID { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }

        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(400)]
        [Required]
        public string Position { get; set; }

        [StringLength(2000)]
        public string Responsibility { get; set; }

        [StringLength(2000)]
        public string Requirements { get; set; }

        [StringLength(256)]
        public string Salary { get; set; }

        [StringLength(1000)]
        [Required]
        public string Contacts { get; set; }

        [ForeignKey("VacancyStatus")]
        public int VacancyStatusID { get; set; }
        public virtual VacancyStatus VacancyStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        [StringLength(256)]
        public string RejectedReasonText { get; set; }

      
    }
}
