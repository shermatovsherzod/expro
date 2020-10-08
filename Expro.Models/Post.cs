using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Models
{
    public class Post : BaseModelAuthorable
    {
        public string Title { get; set; }

        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
