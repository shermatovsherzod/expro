using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class UserPurchasedDocument : BaseModel
    {
        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Document")]
        public int DocumentID { get; set; }
        public virtual Document Document { get; set; }

        public DateTime DatePurchased { get; set; }

        public int Price { get; set; }
    }
}
