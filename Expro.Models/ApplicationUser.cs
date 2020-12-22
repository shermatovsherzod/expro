using Expro.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Expro.Models
{
    public class ApplicationUser : IdentityUser
    {
        //==========================================================     

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string PatronymicName { get; set; }
        public DateTime DateOfBirth { get; set; }                
       
        public DateTime DateRegistered { get; set; }
      
        public UserTypesEnum UserType { get; set; }

        [ForeignKey("Gender")]
        public int? GenderID { get; set; }
        public virtual Gender Gender { get; set; }

        public virtual ICollection<UserLawArea> UserLawAreas { get; set; }

        [ForeignKey("Region")]
        public int? RegionID { get; set; }
        public virtual Region Region { get; set; }

        [ForeignKey("City")]
        public int? CityID { get; set; }
        public virtual City City { get; set; }

        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(256)]
        public string WebSite { get; set; }

        [StringLength(256)]
        public string Fax { get; set; }

        [StringLength(2000)]
        public string AboutMe { get; set; }

        public DateTime? DateSubmittedForApproval { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? DateRejected { get; set; }

        public string RejectedReasonText{ get; set; }

        [ForeignKey("UserStatus")]
        public int UserStatusID { get; set; }
        public virtual UserStatus UserStatus { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        [StringLength(17)]
        public string AccountNumber { get; set; } //лицевой счет

        [InverseProperty("User")]
        public virtual ICollection<UserPurchasedDocument> DocumentsPurchased { get; set; }

        [ForeignKey("Avatar")]
        public int? AvatarID { get; set; }
        public virtual Attachment Avatar { get; set; }

        [InverseProperty("Creator")]
        public virtual ICollection<Attachment> CreatedAttachments { get; set; }

        public bool? IsOnline { get; set; }
        public DateTime? DateLastSeen { get; set; }

        //==========================================================
        [InverseProperty("Author")]
        public virtual ICollection<Post> PostsAuthored { get; set; }
    }
}
