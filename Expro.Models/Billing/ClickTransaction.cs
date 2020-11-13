using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Expro.Models
{
    public class ClickTransaction : BaseModel
    {
        public int ClickTransID { get; set; }
        public int ServiceID { get; set; }
        public int ClickPaydocID { get; set; }
        public string MerchantTransID { get; set; } //litcevoy schot
        public float Amount { get; set; }
        public int Error { get; set; }
        public string ErrorNote { get; set; }
        public string SignTime { get; set; }
        public string SignString { get; set; }
        public int StatusID { get; set; } //0 - pending, 1 - success, 2- cancelled
        public DateTime? DateSuccess { get; set; }
        public DateTime? DateCancelled { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
