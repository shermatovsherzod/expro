using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class ApplicationUser : IdentityUser
    {
        //==========================================================
        public string CustomTag { get; set; }
        public string CustomTag2 { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        [StringLength(256)]
        public string PatronymicName { get; set; }
        public DateTime DateOfBirth { get; set; }     
        public int UserType { get; set; }

        [ForeignKey("Gender")]
        public int? GenderID { get; set; }
        public virtual Gender Gender { get; set; }
     
        //[ForeignKey("LawArea")]
        //public int? LawAreaID { get; set; }
        //public virtual LawArea LawArea { get; set; }

        public ICollection<UserLawArea> UserLawAreas { get; set; }

        //==========================================================
        [InverseProperty("Author")]
        public virtual ICollection<Post> PostsAuthored { get; set; }
    }
}
