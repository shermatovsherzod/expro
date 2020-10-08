﻿using Microsoft.AspNetCore.Identity;
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

        public ICollection<UserLawArea> UserLawAreas { get; set; }

        //[ForeignKey("Region")]
        //public int? RegionID { get; set; }
        //public virtual Region Region { get; set; }

        //[ForeignKey("City")]
        //public int? CityID { get; set; }
        //public virtual City City { get; set; }
        //public string CityOther { get; set; }

        //[InverseProperty("Creator")]
        //public ICollection<Gender> GendersCreated { get; set; }
        //[InverseProperty("Modifier")]
        //public ICollection<Gender> GendersModified { get; set; }

        //==========================================================
        [InverseProperty("Author")]
        public virtual ICollection<Post> PostsAuthored { get; set; }
    }
}
