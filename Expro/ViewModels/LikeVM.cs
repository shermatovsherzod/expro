using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common.Utilities;
using Expro.Models;

namespace Expro.ViewModels
{
    public class LikeCreateVM
    {
        public int ObjectID { get; set; }
        public bool IsPositive { get; set; }
        public string LikeType { get; set; }

        public Like ToModel()
        {
            Like like = new Like()
            {
                IsPositive = this.IsPositive
            };

            return like;
        }
    }
}
