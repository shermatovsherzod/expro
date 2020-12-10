using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Resume : BaseModelAuthorable
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string PatronymicName { get; set; }

        [StringLength(400)]
        [Required]
        public string Contact { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Region")]
        public int? RegionID { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }

        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(256)]
        public string Education { get; set; }
              
        public DateTime? GraduationDate { get; set; }

        [StringLength(256)]
        public string WorkExperience { get; set; }

        [StringLength(256)]
        public string Languages { get; set; }

        [StringLength(256)]
        public string OtherInfo { get; set; }

        [ForeignKey("ResumeStatus")]
        public int ResumeStatusID { get; set; }
        public virtual ResumeStatus ResumeStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        [StringLength(256)]
        public string RejectedReasonText { get; set; }

      
    }
}
