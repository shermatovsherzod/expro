//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace Expro.Models
//{
//    public class DocumentComment : BaseModel
//    {
//        [ForeignKey("Document")]
//        public int DocumentID { get; set; }
//        public virtual Document Document { get; set; }

//        [ForeignKey("Comment")]
//        public int CommentID { get; set; }
//        public virtual Comment Comment { get; set; }

//        public bool IsAnswer { get; set; }
//    }
//}
