using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{    
    public class Company : BaseModelAuthorable
    {
        [Required]
        [StringLength(256)]
        public string CompanyName { get; set; }

        [ForeignKey("Logo")]
        public int? LogoID { get; set; }
        public virtual Attachment Logo { get; set; }

        [ForeignKey("Region")]
        public int? RegionID { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }

        [StringLength(256)]
        public string CityOther { get; set; }

        [ForeignKey("LawAreaParent")]
        public int? LawAreaParentID { get; set; }
        public virtual LawArea LawAreaParent { get; set; }

        public virtual ICollection<CompanyLawArea> CompanyLawAreas { get; set; }

        [StringLength(4000)]
        public string CompanyDescription { get; set; }
              
        [ForeignKey("CompanyStatus")]
        public int CompanyStatusID { get; set; }
        public virtual CompanyStatus CompanyStatus { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        [StringLength(256)]
        public string RejectedReasonText { get; set; }

        [StringLength(100)]
        public string WebSite { get; set; }

        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Address { get; set; }
    }
}
