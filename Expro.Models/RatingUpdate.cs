using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models
{
    public class RatingUpdate : BaseModel
    {
        public DateTime DateUpdated { get; set; }
        public string NextUpdateJobID { get; set; }
    }
}
