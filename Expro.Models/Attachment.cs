﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class Attachment : BaseModelAuthorable
    {
        [Required]
        [StringLength(36)]
        public string GUID { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } //"generatedName (without extension)"

        [Required]
        [StringLength(255)]
        public string DisplayName { get; set; } //"originalName.ext"

        [Required]
        [StringLength(512)]
        public string Path { get; set; } //"path/to/file"

        //[Required]
        [StringLength(32)]
        public string Extension { get; set; }

        public long Size { get; set; } //byte

        [StringLength(128)]
        public string MimeType { get; set; } //"image/jpeg"

        public string UrlParameters { get; set; } //"param1=val1&param2=val2..."

        [InverseProperty("Avatar")]
        public virtual ICollection<ApplicationUser> UsersUsingThisAvatar { get; set; }
    }
}
